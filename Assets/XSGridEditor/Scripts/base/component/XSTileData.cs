/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 额外数据的数据结构 
/// </summary>

using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSTileData : MonoBehaviour, XSITileNode
    {
        /// <summary> 移动消耗 </summary>
        public int Cost = 0;

        public void UpdateEditModePrevPos()
        {
            var dataEdit = this.GetComponent<XSTileDataEditMode>();
            if (dataEdit)
                dataEdit.PrevPos = this.transform.localPosition;
        }

        public void UpdateWorldPos(Vector3 worldPos) => this.transform.position = worldPos;
    }

}
