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
    public class XSGridEditorBrush : GridBrushBase
    {
        [SerializeField]
        public GameObject brushObj;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var brushObj = this.brushObj?.gameObject;
            if (brushObj == null)
                return;

            var mgr = XSEditorInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            var existTile = mgr.GetXSTile(worldPos);
            if (existTile != null)
                return;
                
            GameObject tileObj;
            if (PrefabUtility.IsPartOfPrefabAsset(brushObj))
                tileObj = (GameObject)PrefabUtility.InstantiatePrefab(brushObj, brushTarget.transform) as GameObject;
            else
            {
                tileObj = Instantiate(brushObj, brushTarget.transform);
                tileObj.name = brushObj.name;
            }

            tileObj.transform.position = mgr.WorldToTileCenterWorld(worldPos);
            tileObj.layer = LayerMask.NameToLayer(XSGridDefine.LAYER_TILE);

            //添加到TileDict
            var tile = XSEditorInstance.Instance.GridHelperEditMode?.AddXSTile(tileObj.GetComponent<XSTileNode>());
            if (tile == null)
            {
                Debug.LogError("AddXSTileNode failed");
                GameObject.DestroyImmediate(tileObj);
            }
        }

        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var worldPos = gridLayout.CellToWorld(position);
            var existTile = XSEditorInstance.Instance.GridMgr.GetXSTile(worldPos);
            if (existTile == null || existTile.Node == null)
                return;

            XSEditorInstance.Instance.GridHelperEditMode.RemoveXSTile((XSTileNode)existTile.Node);
        }

        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("FloodFill failed");

        public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout) => Debug.LogWarning("Rotate failed");

        public override void Flip(FlipAxis flip, GridLayout.CellLayout layout) => Debug.LogWarning("Flip failed");

        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot) => Debug.LogWarning("Pick failed");

        public override void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveStart failed");

        public override void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveEnd failed");
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
                return currentStageHandle.FindComponentsOfType<GridLayout>().Where(x => x.gameObject.scene.isLoaded && x.gameObject.activeInHierarchy).Select(x => x.gameObject).ToArray();
            }
        }

    }
}

#endif