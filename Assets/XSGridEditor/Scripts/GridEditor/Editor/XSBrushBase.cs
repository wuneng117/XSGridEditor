/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: brush base class
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
        [SerializeField]
        protected string objPath = "";
        public string ObjPath { get => this.objPath; protected set => this.objPath = value; }

        [SerializeField]
        protected GameObject brushObj;
        public GameObject BrushObj { get => brushObj; set => brushObj = value; }

        [SerializeField]
        protected Vector3Int rotate;
        public Vector3Int Rotation { get => rotate; set => rotate = value; }

        protected string defaultObjPath = "";

        /// <summary> 把当前选择序列化存起来 </summary>
        [HideInInspector]
        [SerializeField]
        protected int selGridInt;
        public int SelGridInt { get => selGridInt; set => selGridInt = value; }

        public Transform BrushParent { get; set; }

        public virtual void Awake()
        {
            if (this.ObjPath == "")
            {
                this.ObjPath = this.defaultObjPath;
            }
        }

        protected virtual GameObject AddGameObject(GridLayout gridLayout, Vector3Int position, string layerName)
        {
            var obj = this.BrushObj?.gameObject;
            if (obj == null)
                return null;

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
            tileObj.transform.Rotate(this.rotate);
            tileObj.layer = LayerMask.NameToLayer(layerName);
            return tileObj;
        }

        protected virtual bool IsExistTile(GridLayout gridLayout, Vector3Int position)
        {
            var mgr = XSInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            var ret = mgr.GetXSTile(worldPos);
            return ret;
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
        /// <summary> target cache </summary>
        protected T Instance { set; get; }

        /// <summary> List of available prefab </summary>
        protected List<GameObject> BrushObjList { get; set; } = new List<GameObject>();

        /// <summary> Currently selected prefab Index </summary>
        protected int selGridInt = 0;

        protected string tempObjPath = "";

        /// <summary> prefab preview size </summary>
        protected static Vector2Int UNIT_SIZE = new Vector2Int(60, 60);

        public override bool canChangeZPosition { get => false; }

        /// <summary> Y where the respective preview starts </summary>
        protected int gridOffY;

        public virtual void Awake()
        {
            this.Instance = (T)this.target;
            Debug.Assert(this.Instance != null);
            this.LoadBrushObjList();
            this.selGridInt = this.Instance.SelGridInt;
            this.gridOffY = 150;
        }

        public virtual void LoadBrushObjList()
        {
            this.tempObjPath = this.Instance.ObjPath;
            this.BrushObjList = XSUE.LoadGameObjAtPath<TCOMP>(new string[] { this.tempObjPath }, "t:Prefab");
        }

        public override void OnPaintInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            EditorGUI.EndChangeCheck();

            if (this.tempObjPath != this.Instance.ObjPath)
            {
                this.LoadBrushObjList();
            }

            int offX = 25;
            this.selGridInt = this.DrawUnitGrid(offX, this.gridOffY);
            if (this.selGridInt != -1)
            {
                if (this.selGridInt >= this.BrushObjList.Count)
                {
                    this.selGridInt = 0;
                }
                this.Instance.BrushObj = this.BrushObjList[this.selGridInt];
                this.Instance.SelGridInt = selGridInt;
            }
        }

        protected virtual int DrawUnitGrid(int offX, int offY)
        {
            if (this.BrushObjList.Count == 0)
                return -1;

            var textList = this.BrushObjList.Select(unit => AssetPreview.GetAssetPreview(unit) as Texture).ToList();
            int width = (int)EditorGUIUtility.currentViewWidth - offX * 2;
            int xCount = width / UNIT_SIZE.x;
            int yCount = (textList.Count - 1) / xCount + 1;
            return GUI.SelectionGrid(new Rect(offX, offY, width, yCount * UNIT_SIZE.x), this.selGridInt, textList.ToArray(), xCount);
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