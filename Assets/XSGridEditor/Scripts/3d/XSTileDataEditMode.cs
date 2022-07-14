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
    [RequireComponent(typeof(XSTileData))]
    [ExecuteInEditMode]
    public class XSTileDataEditMode : MonoBehaviour
    {
        private XSTileData TileData { get; set; }

        /// <summary> 编辑器模式下记录上一次的坐标 </summary>
        protected Vector3 PrevPos { get; set; }

        void Start()
        {
            if (XSU.IsEditor())
            {
                this.TileData = this.GetComponent<XSTileData>();
            }
            else
                this.enabled = false;
                
        }

        // Update is called once per frame
        void Update()
        {
            if (XSU.IsEditor())
            {
                if (this.TileData && this.TileData.Tile != null)
                {
                    this.transform.position = this.TileData.Tile.WorldPos;
                }
            }
        }
    }
}
