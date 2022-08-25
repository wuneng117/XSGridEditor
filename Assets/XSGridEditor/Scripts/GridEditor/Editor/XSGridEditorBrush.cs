/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
#if ENABLE_TILEMAP
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    [CustomGridBrush(true, false, true, "XSGridEditor Brush")]
    public class XSGridEditorBrush : XSBrushBase
    {
        public virtual void Awake()
        {
            this.UnitPath = "Assets/XSGridEditor/Resources/Prefabs/Tiles";

            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var tileRoot = currentStageHandle.FindComponentOfType<XSTileRootCpt>();
            this.BrushParent = tileRoot?.transform;
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if (this.BrushObj?.gameObject == null)
                return;

            if (this.IsExistTile(gridLayout, position))
                return;


            var tileObj = this.AddGameObject(gridLayout, position, XSGridDefine.LAYER_TILE);
            if (tileObj == null)
                return;

            //添加到TileDict
            var tile = XSInstance.Instance.GridHelperEditMode?.AddXSTile(tileObj.GetComponent<XSITileNode>());
            if (tile == null)
            {
                // Debug.LogError("AddXSTileNode failed");
                GameObject.DestroyImmediate(tileObj);
            }
        }

        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var worldPos = gridLayout.CellToWorld(position);
            XSInstance.Instance.GridHelperEditMode.RemoveXSTile(worldPos);
        }

    }

    [CustomEditor(typeof(XSGridEditorBrush))]
    public class XSGridEditorBrushEditor : XSBrushBaseEditor<XSGridEditorBrush, XSITileNode>
    {
    }
}

#endif