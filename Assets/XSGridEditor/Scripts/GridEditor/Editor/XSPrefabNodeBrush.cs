/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
#if ENABLE_TILEMAP
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    [CustomGridBrush(true, false, false, "XSPrefabNode Brush")]
    public class XSPrefabNodeBrush : XSNodeBrushBase<XSPrefabNode>
    {
        [SerializeField]
        protected Transform newBrushParent;
        public Transform NewBrushParent { get => this.newBrushParent; }

        public override void Awake()
        {
            this.defaultObjPath = "Assets/XSGridEditor/Resources/Prefabs/PrefabBrushs";
            base.Awake();
            this.BrushParent = this.NewBrushParent;
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if (this.BrushObj?.gameObject == null)
                return;

            var mgr = this.GetMgr();
            if (mgr == null)
                return;

            var unitObj = this.AddGameObject(gridLayout, position, XSGridDefine.LAYER_UNIT);
            if (unitObj == null)
                return;

            //添加到UnitDict
            var node = unitObj.AddComponent<XSPrefabNode>();
            XSInstance.Instance.GridHelperEditMode?.SetPrefabToNearTerrain(unitObj.transform);
            var ret = mgr.Add(node);
            if (!ret)
            {
                // Debug.LogError("AddXSUnit failed");
                GameObject.DestroyImmediate(unitObj);
            }
        }

        protected override XSINodeMgr<XSPrefabNode> GetMgr() => XSUEE.GetMain()?.PrefabNodeMgr;
    }

    [CustomEditor(typeof(XSPrefabNodeBrush))]
    public class XSPrefabNodeBrushEditor : XSBrushBaseEditor<XSPrefabNodeBrush, Transform>
    {
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