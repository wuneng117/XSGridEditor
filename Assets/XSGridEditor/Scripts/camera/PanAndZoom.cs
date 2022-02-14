/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/9/23
/// @Description: 摄像机移动脚本
/// </summary>
using UnityEngine;
using Cinemachine;
using XSSLG;
using System.Collections;
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

        public bool CanFreeMove { set; get; } = true;

        /// <summary> 是否正在移动到某个位置，这个时候不能通过鼠标控制 </summary>
        private bool IsMoving { set; get; }
        /************************* 变量  end  ***********************/

        private void Awake()
        {
            this.InputProvider = this.GetComponent<CinemachineInputProvider>();
            this.VirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
            this.Confier = this.GetComponent<CinemachineConfiner>();
            this.CameraTransform = this.VirtualCamera.VirtualCameraGameObject.transform;
        }

        // Update is called once per frame
        void Update()
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
            }
        }

        public void ZoomScreen(float increment)
        {
            var position = this.CameraTransform.position;
            Vector3 targetPosition = position + (-1) * this.CameraTransform.forward * increment * this.zoomSpeed;
            targetPosition = Vector3.Lerp(this.CameraTransform.position,
                                                    targetPosition,
                                                    Time.deltaTime);

            var bounds = this.Confier.m_BoundingVolume.bounds;
            if (targetPosition.y < bounds.min.y || targetPosition.y > bounds.max.y)
                return;

            this.CameraTransform.position = targetPosition;
        }

        public Vector3 PanDirection(float x, float y)
        {
            Vector3 direction = Vector3.zero;
            if (y >= Screen.height * 0.95f)
                direction.z -= 1;
            else if (y <= Screen.height * 0.05f)
                direction.z += 1;

            if (x >= Screen.width * 0.95f)
                direction.x -= 1;
            else if (x <= Screen.width * 0.05f)
                direction.x += 1;


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
            var bounds = this.Confier.m_BoundingVolume.bounds;
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);
            targetPosition.z = Mathf.Clamp(targetPosition.z, bounds.min.z, bounds.max.z);

            this.CameraTransform.position = targetPosition;
        }

        /// <summary>
        /// 摄像机保持角度看向指定世界坐标
        /// 在xz平面上计算当前看向的世界坐标和目标距离，就是摄像机移动的距离
        /// </summary>
        /// <param name="worldPos"></param>
        public void MoveTo(Vector3 worldPos) => this.StartCoroutine(MovementAnimation(worldPos));

        // public void Follow(Transform obj)
        // {
        //     this.VirtualCamera.Follow = obj;
        // }


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
