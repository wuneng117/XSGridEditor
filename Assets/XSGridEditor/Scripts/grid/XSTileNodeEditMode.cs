/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: script added to tile gameobject, to edit grid
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary> Editor Actions for XSTileNode </summary>
    [RequireComponent(typeof(XSITileNode))]
    [ExecuteInEditMode]
    public class XSTileNodeEditMode : MonoBehaviour
    {
        /// <summary> Record the last coordinates in editor mode </summary>
        public Vector3 PrevPos { get; set; }

        public virtual void Start()
        {
            if (XSU.IsEditor())
            {
                this.PrevPos = this.transform.localPosition;
            }
            else
            {
                this.enabled = false;
            }
                
        }

        public virtual void Update()
        {
            if (XSU.IsEditor())
            {
                this.transform.localPosition = this.PrevPos;
            }
        }
    }
}
