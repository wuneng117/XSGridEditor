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
using System.Linq;

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

        /// <summary>
        /// 
        /// </summary>
        public static void XSInitToggle(List<ToolbarToggle> toggleList, Action<int> toggleClickFunc)
        {
            if (toggleList == null || toggleList.Count == 0)
            {
                return;
            }

            toggleList.ForEach(toggle =>
            {
                toggle.RegisterValueChangedCallback(evt =>
                {
                    var target = evt.target as ToolbarToggle;
                    if (target == null)
                    {
                        return;
                    }

                    if (evt.newValue)
                    {
                        toggleList.Where(toggle => toggle != evt.target).ToList().ForEach(toggle => toggle.SetValueWithoutNotify(false));
                        if (toggleClickFunc != null)
                        {
                            toggleClickFunc(target.tabIndex + 1);
                        }
                    }
                    else
                    {
                        toggle.SetValueWithoutNotify(true);
                    }
                });
            });


            toggleList[0].value = true;
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