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
    [CustomGridBrush(true, false, false, "XSUnitNode Brush")]
    public class XSUnitNodeBrush : GridBrushBase
    {
        [SerializeField]
        public GameObject brushObj;
        void Start()
        {
            Debug.Log("XSUnitNodeBrush Start");
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var brushObj = this.brushObj?.gameObject;
            if (brushObj == null)
                return;

            var mgr = XSEditorInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            var existTile = mgr.GetXSTile(worldPos);
            if (existTile == null)
                return;

            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var main = currentStageHandle.FindComponentOfType<XSMain>();
            var unitMgr = main.UnitMgr;
            if (unitMgr == null)
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
            tileObj.layer = LayerMask.NameToLayer(XSGridDefine.LAYER_UNIT);

            //添加到TileDict
            var ret = unitMgr.AddXSUnit(tileObj.GetComponent<XSUnitNode>());
            if (!ret)
            {
                Debug.LogError("AddXSUnit failed");
                GameObject.DestroyImmediate(tileObj);
            }
        }

        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var worldPos = gridLayout.CellToWorld(position);
            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var main = currentStageHandle.FindComponentOfType<XSMain>();
            var unitMgr = main.UnitMgr;
            if (unitMgr == null)
                return;
                
            unitMgr.RemoveXSUnit(worldPos);
        }

        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("FloodFill failed");

        public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout) => Debug.LogWarning("Rotate failed");

        public override void Flip(FlipAxis flip, GridLayout.CellLayout layout) => Debug.LogWarning("Flip failed");

        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot) => Debug.LogWarning("Pick failed");

        public override void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveStart failed");

        public override void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveEnd failed");
    }

    [CustomEditor(typeof(XSUnitNodeBrush))]
    public class XSUnitNodeBrushEditor : GridBrushEditorBase
    {
        public override bool canChangeZPosition { get => false; }

        public override GameObject[] validTargets
        {
            get
            {
                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                return currentStageHandle.FindComponentsOfType<GridLayout>().Select(grid => grid.gameObject)
                                                                            .Where(gameObject => gameObject.scene.isLoaded && gameObject.activeInHierarchy && gameObject.name == "UnitRoot").ToArray();
            }
        }

    }
}

#endif