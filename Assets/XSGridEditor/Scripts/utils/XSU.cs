/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: Common unity methods
/// </summary>
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    /// <summary> Common unity methods </summary>
    [ExecuteInEditMode]
    public class XSU
    {
        protected XSU() { }

        /// <summary> is it in unity editor mode</summary>
        public static bool IsEditor() => Application.isEditor && !Application.isPlaying;

        public static XSIGridMgr GridMgr { get => XSU.GetGridMain().GridMgr; }

        public static XSGridHelper GridHelper { get => XSU.GetGridMain().GridHelper; }
        
        protected static XSGridMain gridMain;

        protected static XSGridMain GetGridMain()
        {
            if (XSU.gridMain == null)
            {
                if (XSU.IsEditor())
                {
                    StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                    XSU.gridMain = currentStageHandle.FindComponentOfType<XSGridMain>();
                }
                {
                    XSU.gridMain = Component.FindObjectOfType<XSGridMain>();
                }
            }
            return XSU.gridMain;
        }

        /// <summary>
        /// 清除标记
        /// </summary>
        /// <param name="x">存所有标记的值</param>
        /// <param name="flag">要清除的标记</param>
        public static void ClearFlag(ref int x, int flag)
        {
            x ^= flag;
            x &= (~flag);
        }

        /// <summary>
        /// 添加标记
        /// </summary>
        /// <param name="x">存所有标记的值</param>
        /// <param name="flag">要添加的标记</param>
        public static void SetFlag(ref int x, int flag) => x |= flag;

        /// <summary>
        /// 判断是否有标记
        /// </summary>
        /// <param name="x">存所有标记的值</param>
        /// <param name="flag">要判断的标记</param>
        public static bool GetFlag(ref int x, int flag) => (x & flag) != 0;

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
        /// 世界坐标转为屏幕坐标
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <returns></returns>
        public static Vector3 WorldPosToScreenPos(Vector3 worldPos, Camera camera = null)
        {
            camera = camera ?? XSU.GetMainCamera();
            if (camera == null)
                return new Vector3(0, 0, 0);

            var screenPos = camera.WorldToScreenPoint(worldPos);
            return screenPos;
        }

        /// <summary>
        /// Returns the first Camera in the scene, the name is SceneCamera is the system (it is not clear what it is used for), not the general camera in the scene
        /// </summary>
        /// <returns></returns>
        public static Camera GetMainCamera() => Camera.main;

        /// <summary>
        /// Get the object the mouse is pointing at
        /// </summary>
        /// <param name="screenPos"></param>
        /// <param name="layerName">Which layer the mouse ray intersects with</param>
        /// <param name="camera">The main view camera, if this parameter is not passed in, it will be set to the first found Camera component in the scene</param>
        /// <returns></returns>
        protected static RaycastHit GetMouseHit(Vector2 screenPos, string layerName, Camera camera)
        {
            if (camera == null)
            {
                return new RaycastHit();
            }

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(screenPos);
            if (layerName == "" || layerName == null)
            {
                Physics.Raycast(ray, out hit);
            }
            else
            {
                var index = LayerMask.NameToLayer(layerName);
                Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << index);
            }

            return hit;
        }

        protected static RaycastHit GetMouseHit(Vector2 screenPos, string layerName) => XSUG.GetMouseHit(screenPos, layerName, XSUG.GetMainCamera());

        public static RaycastHit GetMouseHit(Vector2 screenPos) => XSUG.GetMouseHit(screenPos, "");

        /// <summary>
        /// action all chilren
        /// </summary>
        /// <param name="obj">parent gameobject</param>
        public static void ActionChildren(GameObject obj, Action<GameObject> action)
        {
            if (obj == null)
            {
                return;
            }

            int childCount = obj.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                action(obj.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// remove all children
        /// </summary>
        /// <param name="obj">parent gameobject</param>
        public static void RemoveChildren(GameObject obj)
        {
            if (XSU.IsEditor())
            {
                ActionChildren(obj, child => Undo.DestroyObjectImmediate(child));
            }
            else
            {
                ActionChildren(obj, child => GameObject.Destroy(child));
            }
        }

        public static void RemoveObj(GameObject obj)
        {
            if (XSU.IsEditor())
            {
                Undo.DestroyObjectImmediate(obj);
            }
            else
            {
                GameObject.Destroy(obj);
            }
        }
    }
}