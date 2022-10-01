/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 16:33:13
/// @Description: menu bar
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
                XSUEE.ShowTip("XSGridHelperEditor is please delete it first");
                return;
            }

            // create from prefab
            var ret = XSResLoadMgr.LoadGameObject("Prefabs/XSGridEditor");
            if (ret == null)
            {
                XSUEE.ShowTip("cannot find Prefabs/XSGridEditor");
                return;
            }

            ret.name = "XSGridEditor";

            XSAssetPostprocessor.CheckLayer();
        }
    }
}