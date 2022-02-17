/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/9/23
/// @Description: 摄像机移动脚本
/// </summary>
using UnityEngine;
using Cinemachine;
using XSSLG;
using System.Collections;
using UnityEngine.InputSystem;

namespace XSSLG
{
    /// <summary> 摄像机移动脚本 </summary>
    [RequireComponent(typeof(CinemachineInputProvider))]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    [RequireComponent(typeof(CinemachineConfiner))]
    public class PanAndZoom : MonoBehaviour
    {
        /************************* 变量 begin ***********************/

        [SerializeField]
        /// <summary> 边缘移动速度 </summary>
        private float panSpeed = 30f;

        [SerializeField]
        /// <summary> 放大缩小速度 </summary>
        private float zoomSpeed = 20f;

        /// <summary> cinema输入组件 </summary>
        private CinemachineInputProvider InputProvider { set; get; } = null;
        /// <summary> cinema虚拟相机组件 </summary>
        private CinemachineVirtualCamera VirtualCamera { set; get; } = null;
        /// <summary> cinema虚拟相机对象 </summary>
        private Transform CameraTransform { set; get; } = null;
        /// <summary> cinema虚拟相机限制范围 </summary>
        private CinemachineConfiner Confier { set; get; } = null;

        /// <summary> 摄像机是否可以移动 </summary>
        public bool CanFreeMove { set; get; } = true;

        /// <summary> 是否正在移动到某个位置，这个时候不能通过鼠标控制 </summary>
        private bool IsMoving { set; get; } = false;
        /************************* 变量  end  ***********************/

        private void Awake()
        {
            this.InputProvider = this.GetComponent<CinemachineInputProvider>();
            this.VirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
            this.Confier = this.GetComponent<CinemachineConfiner>();
            this.CameraTransform = this.VirtualCamera.VirtualCameraGameObject.transform;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!this.IsMoving && this.CanFreeMove)
            {
                float x = this.InputProvider.GetAxisValue(0);
                float y = this.InputProvider.GetAxisValue(1);
                float z = this.InputProvider.GetAxisValue(2);
                if (x != 0 || y != 0)
                    this.PanScreen(x, y);

                if (z != 0)
                    this.ZoomScreen(z);

                this.UpdateCameraRotation();
            }
        }

        private void UpdateCameraRotation()
        {
            var eulerAngles = this.CameraTransform.rotation.eulerAngles;
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                this.CameraTransform.rotation = Quaternion.Euler(Mathf.Clamp(eulerAngles.x - 22.5f, 22.5f, 90.0f), eulerAngles.y, eulerAngles.z);
                UnityGameUtils.Log("upArrowKey");
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                this.CameraTransform.rotation = Quaternion.Euler(Mathf.Clamp(eulerAngles.x + 22.5f, 22.5f, 90.0f), eulerAngles.y, eulerAngles.z);
                UnityGameUtils.Log("downArrowKey");
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                this.CameraTransform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y - 22.5f, eulerAngles.z);
                UnityGameUtils.Log("leftArrowKey");
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                this.CameraTransform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + 22.5f, eulerAngles.z);
                UnityGameUtils.Log("rightArrowKey");
            }
        }

        /// <summary>
        /// 鼠标滚轮控制地图放大缩小
        /// </summary>
        /// <param name="increment"></param>
        public void ZoomScreen(float increment)
        {
            var position = this.CameraTransform.position;
            Vector3 targetPosition = position - this.CameraTransform.forward * increment * this.zoomSpeed;
            targetPosition = Vector3.Lerp(this.CameraTransform.position,
                                                    targetPosition,
                                                    Time.deltaTime);

            var bounds = this.Confier.m_BoundingVolume.bounds;
            if ((targetPosition.y < bounds.min.y || targetPosition.y > bounds.max.y) ||
                (targetPosition.x < bounds.min.x || targetPosition.x > bounds.max.x) ||
                (targetPosition.z < bounds.min.z || targetPosition.z > bounds.max.z))
                return;

            this.CameraTransform.position = targetPosition;
        }

        public Vector3 PanDirection(float x, float y)
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

            direction = Quaternion.Euler(0, this.CameraTransform.rotation.eulerAngles.y, 0) * direction;
            return direction;
        }

        public void PanScreen(float x, float y)
        {
            var direction = this.PanDirection(x, y);
            this.UpdatePos(direction.normalized);
        }

        private void UpdatePos(Vector3 direction)
        {
            var targetPosition = this.CameraTransform.position + direction * this.panSpeed;
            targetPosition = Vector3.Lerp(this.CameraTransform.position,
                                                    targetPosition,
                                                    Time.deltaTime);
            this.SetCameraPosition(targetPosition);
        }

        private void SetCameraPosition(Vector3 targetPosition)
        {
            // var bounds = this.Confier.m_BoundingVolume.bounds;
            // if ((targetPosition.y < bounds.min.y || targetPosition.y > bounds.max.y) ||
            //     (targetPosition.x < bounds.min.x || targetPosition.x > bounds.max.x) ||
            //     (targetPosition.z < bounds.min.z || targetPosition.z > bounds.max.z))
            //     return;
            // var bounds = this.Confier.m_BoundingVolume.bounds;
            // var screenPos = UnityGameUtils.WorldPosToScreenPos(Vector3.zero);
            // var height = Screen.height;
            // var width = Screen.width;
            // if (screenPos.x > Screen.width || screenPos.y > Screen.height)
            //     return;
            this.CameraTransform.position = targetPosition;
        }

        /// <summary>
        /// 摄像机保持角度看向指定世界坐标
        /// 在xz平面上计算当前看向的世界坐标和目标距离，就是摄像机移动的距离
        /// </summary>
        /// <param name="worldPos"></param>
        public void MoveTo(Vector3 worldPos) => this.StartCoroutine(MovementAnimation(worldPos));

        /// <summary> 携程函数处理移动 </summary>
        public virtual IEnumerator MovementAnimation(Vector3 worldPos)
        {
            this.IsMoving = true;
            var pos = UnityGameUtils.ScreenPosToWorldPos(new Vector2(Screen.width, Screen.height) / 2);
            var targetPos = this.CameraTransform.position + worldPos - pos;
            var dir = -(worldPos - pos).normalized;
            while (true)
            {
                var prevPos = UnityGameUtils.ScreenPosToWorldPos(new Vector2(Screen.width, Screen.height) / 2);
                var direction = worldPos - prevPos;
                if (Vector3.Dot(direction, dir) < 0)
                    break;
                this.UpdatePos(dir);
                yield return 0;
            }
            this.SetCameraPosition(targetPos);
            this.IsMoving = false;
        }
    }
}
