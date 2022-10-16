/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/9/23
/// @Description: camera movement script
/// </summary>
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

namespace XSSLG
{
    // <summary>
    /// camera movement script
    /// </summary>
    [RequireComponent(typeof(CinemachineInputProvider))]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    [RequireComponent(typeof(CinemachineConfiner))]
    public class XSCamera : MonoBehaviour
    {
        /************************* variable begin ***********************/

        [SerializeField]
        /// <summary> screen move speed when mouse in the edge of screen </summary>
        protected float moveSpeed = 30f;

        [SerializeField]
        /// <summary> camera zoom speed </summary>
        protected float zoomSpeed = 20f;

        [SerializeField]
        /// <summary> Camera up and down movement range </summary>
        protected float cameraSizeY = 20;

        [SerializeField]
        ///<summary> Rotation angle of arrow key </summary>
        protected float RotationStep = 22.5f;

        /// <summary> cinemachine input script </summary>
        protected CinemachineInputProvider InputProvider { set; get; }
        /// <summary> cinemachine virtual camera script </summary>
        protected CinemachineVirtualCamera VirtualCamera { set; get; }
        /// <summary> cinemachine virtual camera move range </summary>
        protected CinemachineConfiner Confier { set; get; }

        /// <summary> camera can move </summary>
        public bool CanFreeMove { set; get; } = true;

        /// <summary> camera is moving, if is moving we cannot use mouse to control camera </summary>
        public bool IsMoving { protected set; get; }
        /// <summary> 如果要相机依次移动到多个位置, 就放入列表, 依次移动 </summary>
        protected Queue<Vector3> MoveList = new Queue<Vector3>();

        /// <summary> tile map size </summary>
        public Bounds Bound { get; set; } = new Bounds();
        /************************* variable  end  ***********************/

        public virtual void Awake()
        {
            this.InputProvider = this.GetComponent<CinemachineInputProvider>();
            this.VirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
            this.Confier = this.GetComponent<CinemachineConfiner>();
        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (!this.IsMoving && this.CanFreeMove)
            {
                float x = this.InputProvider.GetAxisValue(0);
                float y = this.InputProvider.GetAxisValue(1);
                float z = this.InputProvider.GetAxisValue(2);
                if (!Mathf.Approximately(x, 0) || !Mathf.Approximately(y, 0))
                {
                    this.MoveScreen(x, y);
                }

                if (!Mathf.Approximately(z, 0))
                {
                    this.ZoomScreen(z);
                }

                this.UpdateCameraRotation();
            }
        }

        protected virtual void MoveScreen(float x, float y)
        {
            var direction = this.MoveDirection(x, y);
            this.UpdatePos(direction.normalized);
        }

        /// <summary>
        /// caculate camera direction
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected virtual Vector3 MoveDirection(float x, float y)
        {
            Vector3 direction = Vector3.zero;
            if (y >= Screen.height * 0.95f)
            {
                direction.z += 1;
            }
            else if (y <= Screen.height * 0.05f)
            {
                direction.z -= 1;
            }

            if (x >= Screen.width * 0.95f)
            {
                direction.x += 1;
            }
            else if (x <= Screen.width * 0.05f)
            {
                direction.x -= 1;
            }

            // The direction of movement changes based on the camera's y-axis rotation angle
            direction = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0) * direction;
            return direction;
        }

        /// <summary>
        /// update camera position
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void UpdatePos(Vector3 direction)
        {
            var targetPosition = this.transform.position + direction * this.moveSpeed;
            targetPosition = Vector3.Lerp(this.transform.position,
                                                    targetPosition,
                                                    Time.deltaTime);
            this.SetCameraPosition(targetPosition);
        }

        /// <summary>
        /// The mouse wheel controrl to zoom in and out
        /// </summary>
        /// <param name="increment"></param>
        protected virtual void ZoomScreen(float increment)
        {
            var position = this.transform.position;
            Vector3 targetPosition = position - this.transform.forward * increment * this.zoomSpeed;
            targetPosition = Vector3.Lerp(this.transform.position,
                                                    targetPosition,
                                                    Time.deltaTime);

            this.SetCameraPosition(targetPosition);
        }

        /// <summary> arrow Key control camera rotation angle </summary>
        protected virtual void UpdateCameraRotation()
        {
            var eulerAngles = this.transform.rotation.eulerAngles;
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                eulerAngles.x = Mathf.Clamp(eulerAngles.x - this.RotationStep, this.RotationStep, 90.0f);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                eulerAngles.x = Mathf.Clamp(eulerAngles.x + this.RotationStep, this.RotationStep, 90.0f);
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                eulerAngles.y -= this.RotationStep;
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                eulerAngles.y += this.RotationStep;
            }
            else
            {
                return;
            }

            this.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
            this.SetConfinerBound(this.Bound);
        }

        protected virtual void FixTargetPosition(ref Vector3 targetPosition)
        {
            if (this.Confier && this.Confier.m_BoundingVolume)
            {
                var bounds = this.Confier.m_BoundingVolume.bounds;
                targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);
                targetPosition.z = Mathf.Clamp(targetPosition.z, bounds.min.z, bounds.max.z);
            }
        }

        protected virtual void SetCameraPosition(Vector3 targetPosition)
        {
            this.FixTargetPosition(ref targetPosition);
            this.transform.position = targetPosition;
        }

        public virtual void SetConfinerBound(Bounds bound)
        {
            this.Bound = bound;
            var collider = (BoxCollider)this.Confier.m_BoundingVolume;

            var halfFov = this.VirtualCamera.m_Lens.FieldOfView * 0.5f;
            var degreeA = 90 - this.transform.eulerAngles.x;
            float sizeY = 0;
            var offset = Vector3.zero;
            if (halfFov >= degreeA)
            {
                sizeY = cameraSizeY;
            }
            // When halfFov < degreeA, cameraSizeY is useless, because collider.transform.position needs to move, otherwise it will not be able to see the full map at the edge
            // We ensure that collider.size is still the same as the tile map size, and then calculate cameraSizeY and collider.transform.position`s Move
            else
            {
                var eulerA = degreeA - halfFov;
                var tanA = Mathf.Tan(eulerA * Mathf.Deg2Rad);

                var eulerB = degreeA + halfFov;
                var tanB = Mathf.Tan(eulerB * Mathf.Deg2Rad);
                sizeY = 2 * collider.transform.position.y * (1 - 2 * tanA / (tanA + tanB));
                var offZ = 2 * collider.transform.position.y * tanB * tanA / (tanA + tanB);
                offset = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0) * (Vector3.back * offZ);
            }

            collider.size = new Vector3(bound.size.x, sizeY, bound.size.z);
            collider.center = new Vector3(bound.center.x + offset.x, 0, bound.center.z + offset.z);
        }

        /// <summary>
        /// 计算相机看向 worldPos 的时相机自己的位置
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        protected virtual Vector3 WorldPosToCameraPos(Vector3 worldPos)
        {
            // 求看向 worldPos 位置的相机所在的位置, 通过已知向量(相机的中心射线方向), 已知相机到平面A的高度, 已知平面A上一点 worldPos, 求得相机位置
            float d = Vector3.Dot(new Vector3(0, this.transform.position.y, 0) - worldPos, new Vector3(0, 1, 0)) / Vector3.Dot(this.transform.forward, new Vector3(0, 1, 0));
            var targetPos = d * this.transform.forward + worldPos;
            this.FixTargetPosition(ref targetPos);
            return targetPos;
        }

        /// <summary>
        /// 相机直接移动到位置
        /// </summary>
        /// <param name="worldPos"></param>
        public virtual void SetPosTo(Vector3 worldPos)
        {
            var targetPos = this.WorldPosToCameraPos(worldPos);
            this.SetCameraPosition(targetPos);
            // Debug.Log("SetPosTo: " + targetPos);
        }

        /// <summary>
        /// 相机以一定速度移动到位置
        /// </summary>
        /// <param name="worldPos"></param>
        public virtual void MoveTo(Vector3 worldPos)
        {
            var targetPos = this.WorldPosToCameraPos(worldPos);
            this.MoveList.Enqueue(targetPos);
            // Debug.Log($"MoveTo {targetPos}");
            if (!this.IsMoving)
            {
                this.StartCoroutine(MovementAnimation());
            }
        }

        /// <summary> 携程函数处理移动 </summary>
        public virtual IEnumerator MovementAnimation()
        {
            while (this.MoveList.Count > 0)
            {
                var targetPos = this.MoveList.Dequeue();
                this.IsMoving = true;
                var dir = (targetPos - this.transform.position).normalized;
                Debug.Log($"targetPos {targetPos} dir {dir}");
                while (true)
                {
                    var direction = targetPos - this.transform.position;
                    Debug.Log($"direction {direction}");
                    if (Vector3.Dot(direction, dir) <= 0)
                        break;
                    this.UpdatePos(dir);
                    Debug.Log($"UpdatePos {this.transform.position}");
                    yield return 0;
                }
                this.SetCameraPosition(targetPos);
            }

            this.IsMoving = false;
        }
    }
}
