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
            XSPopUpWindow.ShowExample(desc);
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
                            toggleClickFunc(target.tabIndex);
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
        public static void XSInit(this BindableElement element, UnityEngine.Object obj, string bindPath)
        {
            if (element != null && bindPath != null && bindPath.Length > 0)
            {
                var serObj = new SerializedObject(obj);
                element.BindProperty(serObj.FindProperty(bindPath));
            }
        }
    }
}