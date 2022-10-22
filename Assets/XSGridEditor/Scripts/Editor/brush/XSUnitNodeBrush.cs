#if ENABLE_TILEMAP
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    [CustomGridBrush(true, false, false, "XSUnitNode Brush")]
    public class XSUnitNodeBrush : XSNodeBrushBase<XSIUnitNode>
    {
        protected override string defaultObjPath { get; } = "Assets/XSGridEditor/Resources/Prefabs/Units";
        public override void Awake()
        {
            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var unitRoot = currentStageHandle.FindComponentOfType<XSUnitRootCpt>();
            this.BrushParent = unitRoot?.transform;
            base.Awake();
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if (this.BrushObj?.gameObject == null)
            {
                return;
            }

            if (!this.IsExistTile(gridLayout, position))
            {
                return;
            }

            var mgr = this.GetMgr();
            if (mgr == null)
            {
                return;
            }

            var unitObj = this.AddGameObject(gridLayout, position, XSGridDefine.LAYER_UNIT);
            if (unitObj == null)
            {
                return;
            }

            var unitEditMode = unitObj.GetComponent<XSUnitNodeEditMode>();
            unitEditMode.enabled = true;

            //add to unit dict
            var unitNode = unitObj.GetComponent<XSIUnitNode>();
            unitNode.GenerateKey();
            var ret = mgr.Add(unitNode);
            if (!ret)
            {
                GameObject.DestroyImmediate(unitObj);
            }
        }

        protected override XSINodeMgr<XSIUnitNode> GetMgr() => XSUE.UnitMgrEditMode;
    }

    [CustomEditor(typeof(XSUnitNodeBrush))]
    public class XSUnitNodeBrushEditor : XSBrushBaseEditor<XSUnitNodeBrush, XSIUnitNode>
    {
    }
}

#endif