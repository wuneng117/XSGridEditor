using UnityEngine;

namespace XSSLG
{
    /// <summary> XSObjectData 的编辑器操作 </summary>
    [RequireComponent(typeof(XSITileNode))]
    [ExecuteInEditMode]
    public class XSTileNodeEditMode : MonoBehaviour
    {
        /// <summary> 编辑器模式下记录上一次的坐标 </summary>
        public Vector3 PrevPos { get; set; }

        public virtual void Start()
        {
            if (XSUE.IsEditor())
            {
                this.PrevPos = this.transform.localPosition;
            }
            else
                this.enabled = false;
                
        }

        public virtual void Update()
        {
            if (XSUE.IsEditor())
            {
                this.transform.localPosition = this.PrevPos;
            }
        }
    }
}
