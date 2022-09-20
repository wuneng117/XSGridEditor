/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: brush to paint prefab
/// </summary>
/// 
#if ENABLE_TILEMAP
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    [CustomGridBrush(true, false, false, "XSPrefabNode Brush")]
    public class XSPrefabNodeBrush : XSNodeBrushBase<XSPrefabNode>
    {
        [SerializeField]
        protected Transform newBrushParent;
        public Transform NewBrushParent { get => this.newBrushParent; protected set => this.newBrushParent = value; }
        /// <summary> optimization, there will be intervals between paints under the same tilepos to prevent excessive stacking </summary>
        protected List<Vector3Int> lastPaintPosList = new List<Vector3Int>();

        public override void Awake()
        {
            this.defaultObjPath = "Assets/XSGridEditor/Resources/Prefabs/PrefabBrushs";
            base.Awake();

            if (this.NewBrushParent == null)
            {
                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                var decorateRoot = currentStageHandle.FindComponentOfType<XSPrefabRootCpt>();
                this.NewBrushParent = decorateRoot?.transform;
            }

            this.BrushParent = this.NewBrushParent;
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if (this.BrushObj?.gameObject == null)
            {
                return;
            }

            var mgr = this.GetMgr();
            if (mgr == null)
                return;

            var gridMgr = XSInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            worldPos = gridMgr.WorldToTileCenterWorld(worldPos);
            var tilePos = gridMgr.WorldToTile(worldPos);
            if (this.lastPaintPosList.Contains(tilePos))
            {
                return;
            }


            var unitObj = this.AddGameObject(gridLayout, position, XSGridDefine.LAYER_UNIT);
            if (unitObj == null)
                return;

            //add to unit dict
            var node = unitObj.AddComponent<XSPrefabNode>();
            XSInstance.Instance.GridHelper?.SetTransToTopTerrain(unitObj.transform, true);
            var ret = mgr.Add(node);
            if (ret)
            {
                if (this.IsExistTile(gridLayout, position))
                {
                    // if ok then remove the tile
                    XSInstance.Instance.GridHelperEditMode?.RemoveXSTile(node.WorldPos);
                }
                this.AddAndDelayDel(tilePos);
            }
            else
            {
                GameObject.DestroyImmediate(unitObj);
            }
        }
        
        async public void AddAndDelayDel(Vector3Int tilePos)
        {
            this.lastPaintPosList.Add(tilePos);
            await Task.Delay(1000);
            this.lastPaintPosList.Remove(tilePos);
        }

        protected override XSINodeMgr<XSPrefabNode> GetMgr() => XSUEE.GetMain()?.PrefabNodeMgr;
    }

    [CustomEditor(typeof(XSPrefabNodeBrush))]
    public class XSPrefabNodeBrushEditor : XSBrushBaseEditor<XSPrefabNodeBrush, Transform>
    {
        public override void Awake()
        {
            base.Awake();
            this.gridOffY = 170;
        }

        public override void OnPaintInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnPaintInspectorGUI();
            this.Instance.BrushParent = this.Instance.NewBrushParent;
            EditorGUI.EndChangeCheck();
        }
    }
}

#endif