/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成XSTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    using TileFunc = Func<Vector3Int, bool>;
    /// <summary> 用于计算的结构 </summary>
    public class XSStepTile : XSTile
    {
        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public XSStepTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node) : base(tilePos, worldPos, cost, node) { }

        public XSStepTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable) : base(tilePos, worldPos, cost, node, isWalkable) { }

        public XSStepTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable, TileFunc canBeDustFunc) : base(tilePos, worldPos, cost, node, isWalkable, canBeDustFunc) { }

        public override bool PassNearRule(XSTile tile, int tileOffYMax)
        {
            //TODO
            return base.PassNearRule(tile, tileOffYMax);
        }
    }
}