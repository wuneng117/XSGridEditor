/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 节点上的组件
/// </summary>

using System;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    // 可通行性，比如 Up 为 true，表示这个节点的上方是可以通行的
    public class Accessibility
    {
        [SerializeField]
        protected bool up = true;
        public bool Up { get => this.up; }

        [SerializeField]
        protected bool down = true;
        public bool Down { get => this.down; }

        [SerializeField]
        protected bool left = true;
        public bool Left { get => this.left; }

        [SerializeField]
        protected bool right = true;
        public bool Right { get => this.right; }
    }

    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSTileNode : MonoBehaviour, XSITileNode
    {
        /// <summary> 移动消耗 </summary>
        [SerializeField]
        protected int cost = 1;

        public int Cost { get => this.cost; }

        /// <summary> 移动消耗 </summary>
        [SerializeField]
        protected Accessibility access;

        public Accessibility Access { get => this.access; }

        public Vector3 WorldPos { get => this.transform.position; set => this.transform.position = value; }

        public int AngleY { get => (int)this.transform.eulerAngles.y; }

        public virtual XSTile CreateXSTile(Vector3Int tilePos) => new XSTile(tilePos, this);

        public virtual void UpdateEditModePrevPos()
        {
            var dataEdit = this.GetComponent<XSTileNodeEditMode>();
            if (dataEdit)
            {
                dataEdit.PrevPos = this.transform.localPosition;
            }
        }

        public virtual void AddBoxCollider(Vector3 tileSize)
        {
            var layer = this.gameObject.layer;
            var tileLayer = LayerMask.NameToLayer(XSGridDefine.LAYER_TILE);
            if (tileLayer == -1)
            {
                Debug.LogWarning("XSTileNode.AddBoxCollider:" + this.transform.position + "tile layer error，please add \"Tile\" to layer");
            }
            else if (tileLayer != layer)
            {
                Debug.LogWarning("XSTileNode.AddBoxCollider:" + this.transform.position + "tile layer error，error，please set layer insteat of \"Tile\"");
            }

            var collider = this.gameObject.AddComponent<BoxCollider>();
            collider.size = tileSize;
        }

        public virtual void RemoveNode() => XSUnityUtils.RemoveObj(this.gameObject);

        public bool IsNull() => this == null;
    }

}
