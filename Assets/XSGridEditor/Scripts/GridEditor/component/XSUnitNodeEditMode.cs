using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> XSObjectData 的编辑器操作 </summary>
    [RequireComponent(typeof(XSIUnitNode))]
    [ExecuteInEditMode]
    public class XSUnitNodeEditMode : MonoBehaviour
    {
        /// <summary> 编辑器模式下记录上一次的坐标 </summary>
        protected Vector3 PrevPos { get; set; }

        public virtual void Start()
        {
            if (!XSUE.IsEditor())
            {
                this.enabled = false;
            }
        }

        public virtual void Update()
        {
            if (XSUE.IsEditor())
            {
                var gridMgr = XSInstance.Instance.GridMgr;
                var pos = gridMgr.WorldToTileCenterWorld(this.transform.position);
                // zero 表示返回的为空，tile获取有问题
                if (pos != Vector3.zero)
                {
                    this.transform.position = pos;
                    XSInstance.Instance.GridHelper.SetTransToTopTerrain(this.transform, true);
                    this.PrevPos = this.transform.position;
                }
                else
                {
                    this.transform.position = this.PrevPos;
                }
            }
        }
    }
}
