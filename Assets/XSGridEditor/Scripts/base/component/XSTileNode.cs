/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 额外数据的数据结构 
/// </summary>

using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSTileNode : MonoBehaviour, XSITileNode
    {
        /// <summary> 移动消耗 </summary>
        public int Cost = 0;

        public void UpdateEditModePrevPos()
        {
            var dataEdit = this.GetComponent<XSTileNodeEditMode>();
            if (dataEdit)
                dataEdit.PrevPos = this.transform.localPosition;
        }

        public void UpdateWorldPos(Vector3 worldPos) => this.transform.position = worldPos;

        public void AddBoxCollider(Vector3 tileSize)
        {
            var layer = this.gameObject.layer;
            var tileLayer = LayerMask.NameToLayer(XSGridDefine.LAYER_TILE);
            if (tileLayer == -1)
                Debug.LogWarning("XSTileNode.AddBoxCollider:" + this.transform.position + "tile layer error，please add \"Tile\" to layer");
            else if (tileLayer != layer)
                Debug.LogWarning("XSTileNode.AddBoxCollider:" + this.transform.position + "tile layer error，error，please set layer insteat of \"Tile\"");

            var collider = this.gameObject.AddComponent<BoxCollider>();
            collider.size = tileSize;
        }
    }

}
