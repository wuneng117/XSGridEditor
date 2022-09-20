/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: read only var
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary> read only var </summary>
    public class XSGridDefine
    {
        protected XSGridDefine() {}

        public static readonly string GAMEOBJECT_TILE_POS_ROOT = "TilePosRoot"; // node name，child is tile pos display text node
        public static readonly string GAMEOBJECT_TILE_COST_ROOT = "TileCostRoot"; // node name，child is tile move cost display text node

        public static readonly string SCENE_GRID_MOVE = "GridMoveRoot"; // node name，child is unit move range display sprite node
        /// <summary> color to present different cost </summary>
        public static readonly Color[] TILE_COST_COLOR = {
            Color.grey,
            Color.white,
            Color.yellow,
            Color.red,
        };

        public static readonly string LAYER_TILE = "Tile";
        public static readonly string LAYER_UNIT = "Unit";
    }
}