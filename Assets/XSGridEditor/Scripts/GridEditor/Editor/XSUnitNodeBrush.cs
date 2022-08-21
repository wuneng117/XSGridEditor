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
    public class XSUnitNodeBrush : XSBrushBase
    {
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if (this.brushObj?.gameObject == null)
                return;

            if (!this.IsExistTile(gridLayout, position))
                return;

            var unitMgr = this.GetUnitMgr();
            if (unitMgr == null)
                return;

            var unitObj = this.AddGameObject(brushTarget.transform, gridLayout, position, XSGridDefine.LAYER_UNIT);
            if (unitObj == null)
                return;

            //添加到UnitDict
            var ret = unitMgr.AddXSUnit(unitObj.GetComponent<XSIUnitNode>());
            if (!ret)
            {
                Debug.LogError("AddXSUnit failed");
                GameObject.DestroyImmediate(unitObj);
            }
        }

        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var unitMgr = this.GetUnitMgr();
            if (unitMgr == null)
                return;

            var worldPos = gridLayout.CellToWorld(position);
            unitMgr.RemoveXSUnit(worldPos);
        }

        protected virtual XSUnitMgr GetUnitMgr()
        {
            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var main = currentStageHandle.FindComponentOfType<XSMain>();
            return main?.UnitMgr;
        }
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