/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: Common unity methods
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> Common unity methods </summary>
    public class XSUnityUtils
    {
        protected XSUnityUtils() {}


        /// <summary> is it in unity editor mode</summary>
        public static bool IsEditor() => Application.isEditor && !Application.isPlaying;

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