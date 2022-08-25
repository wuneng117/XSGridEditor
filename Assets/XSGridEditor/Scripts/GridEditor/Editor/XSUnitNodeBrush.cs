/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
#if ENABLE_TILEMAP
using System.Collections.Generic;
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
        public string UnitPath = "Assets/XSGridEditor/Resources/Prefabs/Units";

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

            var unitEditMode = unitObj.GetComponent<XSUnitNodeEditMode>();
            unitEditMode.enabled = true;

            //添加到UnitDict
            var ret = unitMgr.AddXSUnit(unitObj.GetComponent<XSIUnitNode>());
            if (!ret)
            {
                // Debug.LogError("AddXSUnit failed");
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
        protected XSUnitNodeBrush Instance { set; get; }
        protected List<GameObject> BrushObjList { get; set; } = new List<GameObject>();

        protected int selGridInt = -1;

        protected static Vector2Int UNIT_SIZE = new Vector2Int(60, 60);

        public override bool canChangeZPosition { get => false; }

        public virtual void Awake()
        {
            this.Instance = (XSUnitNodeBrush)this.target;
            if (this.Instance)
                this.BrushObjList = XSUE.LoadGameObjAtPath<XSIUnitNode>(new string[] { this.Instance.UnitPath }, "t:Prefab");
        }

        public override void OnPaintInspectorGUI()
        {
            var brush = (XSUnitNodeBrush)target;

            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            EditorGUI.EndChangeCheck();


            int offY = 100;
            int offX = 25;
            selGridInt = this.DrawUnitGrid(brush, offX, offY);
            if (selGridInt != -1)
                this.Instance.brushObj = this.BrushObjList[selGridInt];

            GUI.EndScrollView();
        }

        protected virtual int DrawUnitGrid(XSUnitNodeBrush brush, int offX, int offY)
        {
            if (this.BrushObjList.Count == 0)
                return -1;

            var textList = this.BrushObjList.Select(unit => AssetPreview.GetAssetPreview(unit) as Texture).ToList();
            int width = (int)EditorGUIUtility.currentViewWidth - offX * 2;
            int xCount = width / UNIT_SIZE.x;
            int yCount = textList.Count / xCount + 1;
            return GUI.SelectionGrid(new Rect(offX, offY, width, yCount * UNIT_SIZE.x), selGridInt, textList.ToArray(), xCount);
        }

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