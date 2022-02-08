/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 静态只读常量定义
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary> 静态只读常量定义 </summary>
    public class XSGridDefine
    {
        public static readonly string GAMEOBJECT_TILE_POS_ROOT = "TilePosRoot"; // 节点名称，用于存放 tilepos 显示文字根节点
        public static readonly string GAMEOBJECT_TILE_COST_ROOT = "TileCostRoot"; // 节点名称，用于存放 tile cost 显示文字根节点

        /// <summary> cost 颜色显示 </summary>
        public static readonly Color[] TILE_COST_COLOR = {
            Color.grey,
            Color.white,
            Color.yellow,
            Color.red,
        };
    }
}