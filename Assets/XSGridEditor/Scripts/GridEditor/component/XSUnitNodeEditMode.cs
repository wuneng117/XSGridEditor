/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: script added to unit gameobject, to edit grid
/// </summary>
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> Editor Actions for XSUnitNode </summary>
    [RequireComponent(typeof(XSIUnitNode))]
    [ExecuteInEditMode]
    public class XSUnitNodeEditMode : MonoBehaviour
    {
        /// <summary> Record the last position in editor mode </summary>
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
                // zero means that the returned value is empty, and there is a problem with tile acquisition
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
