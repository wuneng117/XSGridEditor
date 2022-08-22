/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
#if ENABLE_TILEMAP
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace XSSLG
{
    [CustomGridBrush(true, false, true, "XSGridEditor Brush")]
    public class XSGridEditorBrush : XSBrushBase
    {
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
             StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var curHe = currentStageHandle.FindComponentsOfType<XSGridHelperEditMode>();
            if (this.brushObj?.gameObject == null)
                return;

            if (this.IsExistTile(gridLayout, position))
                return;


            var tileObj = this.AddGameObject(brushTarget.transform, gridLayout, position, XSGridDefine.LAYER_TILE);
            if (tileObj == null)
                return;

            //添加到TileDict
            var tile = XSInstance.Instance.GridHelperEditMode?.AddXSTile(tileObj.GetComponent<XSITileNode>());
            if (tile == null)
            {
                Debug.LogError("AddXSTileNode failed");
                GameObject.DestroyImmediate(tileObj);
            }
        }
    }

    [CustomEditor(typeof(XSGridEditorBrush))]
    public class XSGridEditorBrushEditor : GridBrushEditorBase
    {
        public override bool canChangeZPosition { get => false; }

        public override GameObject[] validTargets
        {
            get
            {
                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                return currentStageHandle.FindComponentsOfType<GridLayout>().Select(grid => grid.gameObject)
                                                                            .Where(gameObject => gameObject.scene.isLoaded && gameObject.activeInHierarchy && gameObject.name == "TileRoot").ToArray();
            }
        }

    }
}

#endif