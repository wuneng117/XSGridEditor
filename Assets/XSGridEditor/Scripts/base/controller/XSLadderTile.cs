/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成XSTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;
using XSGridDefine;

namespace XSSLG
{
    using TileFunc = Func<Vector3Int, bool>;
    /// <summary> 用于计算的结构 </summary>
    public class XSLadderTile : XSTile
    {
        protected XSGridDefine.XSLadderTileType Type { get; }

        // TODO
        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public XSLadderTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, XSGridDefine.XSLadderTileType type) : base(tilePos, worldPos, cost, node)
        {
            this.Type = type;
        }

        public XSLadderTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable, XSGridDefine.XSLadderTileType type) : this(tilePos, worldPos, cost, node, isWalkable) { }

        public XSLadderTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable, TileFunc canBeDustFunc, XSGridDefine.XSLadderTileType type) : base(tilePos, worldPos, cost, node, isWalkable, canBeDustFunc) { }

        public override bool PassNearRule(XSTile tile, int tileOffYMax)
        {
            //TODO
            return base.PassNearRule(tile, tileOffYMax);
        }
    }
}