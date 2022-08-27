/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 节点上的组件
/// </summary>

using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSTileNode : MonoBehaviour, XSITileNode
    {
        /// <summary> 移动消耗 </summary>
        [SerializeField]
        protected int cost = 1;

        public int Cost { get => this.cost; }

        public Vector3 WorldPos { get => this.transform.position; set => this.transform.position = value; }

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
    }

}
