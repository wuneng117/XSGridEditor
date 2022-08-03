/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 常用的 unity 方法 
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 常用的 unity 方法 </summary>
    public class UnityUtils
    {
        /// <summary>
        /// 打印日志统一入口
        /// </summary>
        /// <param name="message">要打印的日志</param>
        public static void Log(object message) => UnityEngine.Debug.Log(message);

        /// <summary> 是否是 unity 编辑器模式下 </summary>
        public static bool IsEditor() => Application.isEditor && !Application.isPlaying;

        /// <summary>
        /// 从 src 到 dest 坐标的旋转
        /// </summary>
        /// <param name="src">要旋转物体的坐标</param>
        /// <param name="dest">要朝向目标的坐标</param>
        /// <returns>src 的 locationRotation 值</returns>
        public static Quaternion RotationTo(Vector3 src, Vector3 dest)
        {
            var distance = dest - src;
            Quaternion rotate = Quaternion.LookRotation(distance);
            return rotate;
        }

        /// <summary>
        /// 获取鼠标所指向的对象
        /// </summary>
        /// <param name="screenPos">屏幕坐标</param>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <param name="layerName">鼠标射线和哪个layer相交，一般和terrian的layer相交</param>
        /// <returns></returns>
        public static RaycastHit GetMouseHit(Vector2 screenPos, Camera camera = null, string layerName = null)
        {
            camera = camera ?? UnityUtils.GetMainCamera();
            if (camera == null)
                return new RaycastHit();

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(screenPos);
            if (layerName != null)
            {
                var index = LayerMask.NameToLayer(layerName);
                Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << index);
            }
            else
                Physics.Raycast(ray, out hit);
            return hit;
        }

        /// <summary>
        /// 世界坐标转为屏幕坐标
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <returns></returns>
        public static Vector3 WorldPosToScreenPos(Vector3 worldPos, Camera camera = null)
        {
            camera = camera ?? UnityUtils.GetMainCamera();
            if (camera == null)
                return new Vector3(0, 0, 0);

            var screenPos = camera.WorldToScreenPoint(worldPos);
            return screenPos;
        }

        /// <summary>
        /// 返回场景中的第一个Camera，名字为SceneCamera是系统的（暂时不清楚什么用），不是场景中的一般camera
        /// </summary>
        /// <returns></returns>
        public static Camera GetMainCamera()
        {
            var cameras = Resources.FindObjectsOfTypeAll<Camera>();
            foreach (var camera in cameras)
            {
                if (camera.name != "SceneCamera")
                    return camera;
            }
            return null;
        }

        /// <summary>
        /// 操作所有子节点
        /// </summary>
        /// <param name="obj">父节点</param>
        public static void ActionChildren(GameObject obj, Action<GameObject> action)
        {
            int childCount = obj.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
                action(obj.transform.GetChild(i).gameObject);
        }

        /// <summary>
        /// 删除所有子节点
        /// </summary>
        /// <param name="obj">父节点</param>
        public static void RemoveChildren(GameObject obj)
        {
            if (UnityUtils.IsEditor())
                ActionChildren(obj, (GameObject child) => Undo.DestroyObjectImmediate(child));
            else
                ActionChildren(obj, (GameObject child) => GameObject.Destroy(child));
        }

        public static void RemoveObj(GameObject obj)
        {
            if (UnityUtils.IsEditor())
                Undo.DestroyObjectImmediate(obj);
            else
                GameObject.Destroy(obj);
        }

        /// <summary>
        /// 隐藏所有子节点
        /// </summary>
        /// <param name="obj">父节点</param>
        public static void HideChildren(GameObject obj) => ActionChildren(obj, (GameObject child) => child.SetActive(false));
    }
}