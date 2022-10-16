/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 20:42:18
/// @Description: 
/// </summary>
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
namespace XSSLG
{

    public class XSUE
    {
        protected XSUE() { }

        protected static XSGridMainEditMode gridMainEditMode;
        public static XSPrefabNodeMgr PrefabNodeMgr { get => XSUE.GetGridMainEditMode()?.PrefabNodeMgr; }

        public static XSUnitMgrEditMode UnitMgrEditMode { get => XSUE.GetGridMainEditMode()?.UnitMgrEditMode; }

        public static XSGridHelperEditMode GridHelperEditMode { get => XSUE.GetGridMainEditMode()?.GridHelperEditMode; }

        /// <summary> pop-up reminder </summary>
        public static void ShowTip(string desc)
        {
            var tip = ScriptableObject.CreateInstance<XSPopUpView>();
            tip.Init(desc);
            tip.ShowPopup();
        }

        public static XSGridMainEditMode GetGridMainEditMode()
        {
            if (XSUE.gridMainEditMode == null)
            {

                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                XSUE.gridMainEditMode = currentStageHandle.FindComponentOfType<XSGridMainEditMode>();
            }
            return XSUE.gridMainEditMode;
        }
    }

    public static class XSUEExtension
    {

        /// <summary>
        /// extension ListView Init
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void XSInit<T>(this ListView s, List<T> itemsSource, string itemAssetPath, Action<VisualElement, T> bindFunc, Action<T> selFunc) where T : class
        {
            s.itemsSource = itemsSource;
            s.makeItem = () =>
                {
                    var node = new VisualElement();
                    var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(itemAssetPath);
                    visualTree?.CloneTree(node);
                    return node;
                };

            s.bindItem = (VisualElement node, int index) =>
                {
                    var obj = s.itemsSource[index];
                    if (obj != null)
                    {
                        bindFunc(node, obj as T);
                    }
                };

            s.onSelectionChange += (IEnumerable<object> obj) =>
                {
                    foreach (var o in obj)
                    {
                        selFunc(o as T);
                    }
                };
        }

        public static void XSInit(this BindableElement s, UnityEngine.Object obj, string bindPath)
        {
            if (s != null && bindPath != null && bindPath.Length > 0)
            {
                var serObj = new SerializedObject(obj);
                s.Bind(serObj);
                s.bindingPath = bindPath;
            }

        }
    }
}