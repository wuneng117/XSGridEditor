/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: brush to paint tile
/// </summary>
/// 
#if ENABLE_TILEMAP
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    [CustomGridBrush(true, false, false, "XSGridEditor Brush")]
    public class XSGridEditorBrush : XSBrushBase
    {
        public override void Awake()
        {
            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var tileRoot = currentStageHandle.FindComponentOfType<XSTileRootCpt>();
            this.BrushParent = tileRoot?.transform;
            
            this.defaultObjPath = "Assets/XSGridEditor/Resources/Prefabs/Tiles";
            base.Awake();
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if (this.BrushObj?.gameObject == null)
                return;

            var tileObj = this.AddGameObject(gridLayout, position, XSGridDefine.LAYER_TILE);
            if (tileObj == null)
                return;

            //添加到TileDict
            var tile = XSInstance.Instance.GridHelperEditMode?.AddXSTile(tileObj.GetComponent<XSITileNode>());
            if (tile == null)
            {
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