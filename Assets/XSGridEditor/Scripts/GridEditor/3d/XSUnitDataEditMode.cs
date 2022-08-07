using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> XSObjectData 的编辑器操作 </summary>
    [RequireComponent(typeof(XSUnitData))]
    [ExecuteInEditMode]
    public class XSUnitDataEditMode : MonoBehaviour
    {
        /// <summary> 编辑器模式下记录上一次的坐标 </summary>
        protected Vector3 PrevPos { get; set; }

        void Start()
        {
            if (!XSUE.IsEditor())
                this.enabled = false;
        }

        void Update()
        {
            if (XSUE.IsEditor())
            {
                // XSU.Log("XSUnitDataEditMode Update");
                var gridMgr = XSEditorInstance.Instance.GridMgr;
                var pos = gridMgr.WorldToTileCenterWorld(this.transform.position);
                // zero 表示返回的为空，tile获取有问题
                if (pos != Vector3.zero)
                {
                    this.transform.position = pos;
                    this.PrevPos = pos;
                }
                else
                {
                    this.transform.position = this.PrevPos;
                }
            }
        }
    }
}
