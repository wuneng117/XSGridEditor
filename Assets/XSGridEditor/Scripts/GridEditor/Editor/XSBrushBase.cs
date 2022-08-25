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
    public class XSBrushBase : GridBrushBase
    {
        public string UnitPath = "";

        protected Transform BrushParent { get; set; }

        public GameObject BrushObj { get; set; }

        protected virtual GameObject AddGameObject(GridLayout gridLayout, Vector3Int position, string layerName)
        {
            var obj = this.BrushObj?.gameObject;
            if (obj == null)
                return null;

            // if (this.BrushParent == null)
            //     return null;

            GameObject tileObj;
            if (PrefabUtility.IsPartOfPrefabAsset(obj))
                tileObj = (GameObject)PrefabUtility.InstantiatePrefab(obj, this.BrushParent) as GameObject;
            else
            {
                tileObj = Instantiate(obj, this.BrushParent);
                tileObj.name = obj.name;
            }

            var mgr = XSInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            tileObj.transform.position = mgr.WorldToTileCenterWorld(worldPos);
            tileObj.layer = LayerMask.NameToLayer(layerName);
            return tileObj;
        }

        protected virtual bool IsExistTile(GridLayout gridLayout, Vector3Int position)
        {
            var mgr = XSInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            var existTile = mgr.GetXSTile(worldPos);
            return existTile != null;
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("Paint failed");

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("Erase failed");

        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("FloodFill failed");

        public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout) => Debug.LogWarning("Rotate failed");

        public override void Flip(FlipAxis flip, GridLayout.CellLayout layout) => Debug.LogWarning("Flip failed");

        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot) => Debug.LogWarning("Pick failed");

        public override void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveStart failed");

        public override void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveEnd failed");
    }

    public class XSBrushBaseEditor<T, TCOMP> : GridBrushEditorBase where T : XSBrushBase
    {
        /// <summary> 操作对象 </summary>
        protected T Instance { set; get; }

        /// <summary> 可以用的笔刷list </summary>
        protected List<GameObject> BrushObjList { get; set; } = new List<GameObject>();

        /// <summary> 当前选中的笔刷Index </summary>
        protected int selGridInt = 0;

        /// <summary> 笔刷预览大小 </summary>
        protected static Vector2Int UNIT_SIZE = new Vector2Int(60, 60);

        public override bool canChangeZPosition { get => false; }

        public virtual void Awake()
        {
            this.Instance = (T)this.target;
            if (this.Instance)
                this.BrushObjList = XSUE.LoadGameObjAtPath<TCOMP>(new string[] { this.Instance.UnitPath }, "t:Prefab");
        }

        public override void OnPaintInspectorGUI()
        {
            base.OnInspectorGUI();

            int offY = 100;
            int offX = 25;
            selGridInt = this.DrawUnitGrid(offX, offY);
            if (selGridInt != -1)
                this.Instance.BrushObj = this.BrushObjList[selGridInt];
        }

        protected virtual int DrawUnitGrid(int offX, int offY)
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
                                                                            .Where(gameObject => gameObject.scene.isLoaded && gameObject.activeInHierarchy && gameObject.name == "TileRoot").ToArray();
            }
        }
    }
}

#endif