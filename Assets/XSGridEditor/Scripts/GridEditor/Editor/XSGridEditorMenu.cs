/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 16:33:13
/// @Description: 菜单栏功能
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    public class XSGridEditorMenu : MonoBehaviour
    {
        [MenuItem("Object/XSGridEditor/CreateGrid")]
        static public void CreateGrid()
        {
            var gridNode = Component.FindObjectOfType<XSGridHelperEditor>();
            if (gridNode != null)
            {
                var tip = ScriptableObject.CreateInstance<PopUpView>();
                tip.Init(new Size(300, 100), "已经存在XSGridHelperEditor节点，请先删除");
                tip.ShowPopup();
                return;
            }

            gridNode = GameObject.Instantiate();
            gridNode.AddComponent<XSGridHelperEditMode>();
            // gridNode.AddComponent<Grid>();
            gridNode.name = "Grid";
        }
    }
}