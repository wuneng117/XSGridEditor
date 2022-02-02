/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 额外数据的数据结构 
/// </summary>

using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSTileData : MonoBehaviour
    {
        /// <summary> 移动消耗 </summary>
        public int Cost = 0;

        /// <summary> GridMgr 初始化后生成对应的 PathFinderTile </summary>
        public PathFinderTile Tile {get; set;}
    }
}
