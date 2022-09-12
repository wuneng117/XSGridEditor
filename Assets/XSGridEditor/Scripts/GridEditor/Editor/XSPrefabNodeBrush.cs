/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
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
        /// <summary> 优化下，在同一个tilepos下paint会有间隔，防止叠加过多 </summary>
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

            //添加到UnitDict
            var node = unitObj.AddComponent<XSPrefabNode>();
            XSInstance.Instance.GridHelper?.SetTransToTopTerrain(unitObj.transform, true);
            var ret = mgr.Add(node);
            if (ret)
            {
                if (this.IsExistTile(gridLayout, position))
                {
                    // 放东西了就把tile删除不能走的
                    XSInstance.Instance.GridHelperEditMode?.RemoveXSTile(node.WorldPos);
                }
                this.AddAndDelayDel(tilePos);
            }
            else
            {
                // Debug.LogError("AddXSUnit failed");
                GameObject.DestroyImmediate(unitObj);
            }
        }
        
        /// <summary> 携程函数处理移动 </summary>
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