/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 16:33:13
/// @Description: 菜单栏功能
/// </summary>
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    public class XSGridEditorMenu : MonoBehaviour
    {
        [MenuItem("Tools/XSGridEditor/CreateGrid")]
        static public void CreateGrid()
        {
            var gridNode = Component.FindObjectOfType<XSGridHelperEditMode>();
            if (gridNode != null)
            {
                XSUEE.ShowTip("已经存在XSGridHelperEditor节点，请先删除");
                return;
            }

            // 从prefab创建引用的gameobject
            var ret = XSResLoadMgr.LoadGameObject("Prefabs/XSGridEditor");
            if (ret == null)
            {
                XSUEE.ShowTip("未找到Prefabs/XSGridEditor");
                return;
            }

            ret.name = "XSGridEditor";

            XSAssetPostprocessor.CheckLayer();
        }
    }
}