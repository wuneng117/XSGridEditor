/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/9/23
/// @Description: 摄像机移动脚本
/// </summary>
using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;

namespace XSSLG
{
    // <summary>
    /// 摄像机移动脚本
    /// 如果要限制移动范围，创建1个BoxCollider，并赋值给 CinemachineConfiner 的 Bounding Volume，设置下高度，并：
    /// var panObj = Component.FindObjectOfType<PanAndZoom>();
    /// Bounds bounds = XXXX;  // 自己计算的地图大小
    /// panObj.SetConfinerBound(bounds);
    /// </summary>
    [RequireComponent(typeof(CinemachineInputProvider))]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    [RequireComponent(typeof(CinemachineConfiner))]
    public class XSCamera : MonoBehaviour
    {
        /************************* 变量 begin ***********************/

        [SerializeField]
        /// <summary> 边缘移动速度 </summary>
        protected float moveSpeed = 30f;

        [SerializeField]
        /// <summary> 放大缩小速度 </summary>
        protected float zoomSpeed = 20f;

        [SerializeField]
        /// <summary> 摄像机上下移动范围 </summary>
        protected float cameraSizeY = 20;

        [SerializeField]
        ///<summary> 按键每次旋转角度 </summary>
        protected float RotationStep = 22.5f;

        /// <summary> cinema输入组件 </summary>
        protected CinemachineInputProvider InputProvider { set; get; } = null;
        /// <summary> cinema虚拟相机组件 </summary>
        protected CinemachineVirtualCamera VirtualCamera { set; get; } = null;
        /// <summary> cinema虚拟相机限制范围 </summary>
        protected CinemachineConfiner Confier { set; get; } = null;

        /// <summary> 摄像机是否可以移动 </summary>
        public bool CanFreeMove { set; get; } = true;

        /// <summary> 是否正在移动到某个位置，这个时候不能通过鼠标控制 </summary>
        protected bool IsMoving { set; get; } = false;

        /// <summary> 地图大小 </summary>
        public Bounds Bound { get; set; } = new Bounds();

        /************************* 变量  end  ***********************/

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
                if (x != 0 || y != 0)
                    this.MoveScreen(x, y);

                if (z != 0)
                    this.ZoomScreen(z);

                this.UpdateCameraRotation();
            }
        }

        /// <summary>
        /// 移动摄像机
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected virtual void MoveScreen(float x, float y)
        {
            var direction = this.MoveDirection(x, y);
            this.UpdatePos(direction.normalized);
        }

        /// <summary>
        /// 计算摄像机移动方向
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected virtual Vector3 MoveDirection(float x, float y)
        {
            Vector3 direction = Vector3.zero;
            if (y >= Screen.height * 0.95f)
                direction.z += 1;
            else if (y <= Screen.height * 0.05f)
                direction.z -= 1;

            if (x >= Screen.width * 0.95f)
                direction.x += 1;
            else if (x <= Screen.width * 0.05f)
                direction.x -= 1;

            // 移动方向会根据摄像机的 y 轴旋转角度而变化
            direction = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0) * direction;
            return direction;
        }

        /// <summary>
        /// 更新摄像机位置
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
        /// 鼠标滚轮控制地图放大缩小
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

        /// <summary> 按键控摄像机旋转角度 </summary>
        protected virtual void UpdateCameraRotation()
        {
            var eulerAngles = this.transform.rotation.eulerAngles;
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
                eulerAngles.x = Mathf.Clamp(eulerAngles.x - this.RotationStep, this.RotationStep, 90.0f);
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                eulerAngles.x = Mathf.Clamp(eulerAngles.x + this.RotationStep, this.RotationStep, 90.0f);
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                eulerAngles.y -= this.RotationStep;
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                eulerAngles.y += this.RotationStep;
            else
                return;
            
            this.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
            this.SetConfinerBound(this.Bound);
        }


        /// <summary> 设置摄像机位置 </summary>
        protected virtual void SetCameraPosition(Vector3 targetPosition)
        {
            if (this.Confier && this.Confier.m_BoundingVolume)
            {
                var bounds = this.Confier.m_BoundingVolume.bounds;
                targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);
                targetPosition.z = Mathf.Clamp(targetPosition.z, bounds.min.z, bounds.max.z);
            }

            this.transform.position = targetPosition;
        }

        // /// <summary>
        // /// 摄像机保持角度看向指定世界坐标
        // /// 在xz平面上计算当前看向的世界坐标和目标距离，就是摄像机移动的距离
        // /// 当前看向的世界坐标和目标都是高度为0的tile所在的坐标
        // /// </summary>
        // /// <param name="worldPos"></param>
        // public virtual void MoveTo(Vector3 worldPos) => this.StartCoroutine(MovementAnimation(worldPos));

        // /// <summary> 携程函数处理移动 </summary>
        // protected virtual IEnumerator MovementAnimation(Vector3 worldPos)
        // {
        //     this.IsMoving = true;
        //     var pos = XSUG.ScreenPosToTileWorldPos(new Vector2(Screen.width, Screen.height) / 2);
        //     var targetPos = this.transform.position + worldPos - pos;
        //     var dir = -(worldPos - pos).normalized;
        //     while (true)
        //     {
        //         var prevPos = XSUG.ScreenPosToTileWorldPos(new Vector2(Screen.width, Screen.height) / 2);
        //         var direction = worldPos - prevPos;
        //         if (Vector3.Dot(direction, dir) < 0)
        //             break;
        //         this.UpdatePos(dir);
        //         yield return 0;
        //     }
        //     this.SetCameraPosition(targetPos);
        //     this.IsMoving = false;
        // }

        public virtual void SetConfinerBound(Bounds bound)
        {
            this.Bound = bound;
            var collider = (BoxCollider)this.Confier.m_BoundingVolume;

            var halfFov = this.VirtualCamera.m_Lens.FieldOfView * 0.5f;
            var degreeA = 90 - this.transform.eulerAngles.x;
            float sizeY = 0;
            var offset = Vector3.zero;
            if (halfFov >= degreeA)
                sizeY = cameraSizeY;
            // 当 halfFov < degreeA 时，cameraSizeY 就没有用了，因为 collider.transform.position 需要移动，否则会导致在边缘时无法看到全部地图
            // 我们保证 collider.size 还是和地图大小保持一致，然后计算 cameraSizeY ，以及 collider.transform.position 的移动
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
            collider.transform.position = new Vector3(bound.center.x + offset.x, collider.transform.position.y, bound.center.z + offset.z);
        }
    }
}
