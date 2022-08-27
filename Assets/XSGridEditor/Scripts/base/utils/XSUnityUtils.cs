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
    public class XSUnityUtils
    {
        protected XSUnityUtils() {}


        /// <summary> 是否是 unity 编辑器模式下 </summary>
        public static bool IsEditor() => Application.isEditor && !Application.isPlaying;

        /// <summary>
        /// 操作所有子节点
        /// </summary>
        /// <param name="obj">父节点</param>
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
        /// 删除所有子节点
        /// </summary>
        /// <param name="obj">父节点</param>
        public static void RemoveChildren(GameObject obj)
        {
            if (XSUnityUtils.IsEditor())
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
            if (XSUnityUtils.IsEditor())
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