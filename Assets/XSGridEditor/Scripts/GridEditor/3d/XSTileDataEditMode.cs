/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/9
/// @Description: tile 上放置的 object 数据结构
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> XSObjectData 的编辑器操作 </summary>
    [ExecuteInEditMode]
    public class XSTileDataEditMode : MonoBehaviour
    {
        /// <summary> 编辑器模式下记录上一次的坐标 </summary>
        public Vector3 PrevPos { get; set; }

        void Start()
        {
            if (XSUE.IsEditor())
            {
                this.PrevPos = this.transform.localPosition;
            }
            else
                this.enabled = false;
                
        }

        // Update is called once per frame
        void Update()
        {
            if (XSUE.IsEditor())
            {
                this.transform.localPosition = this.PrevPos;
            }
        }
    }
}
