/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成PathFInderTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 用于计算的结构 </summary>
    public class XSTile
    {
        /// <summary> 世界坐标 </summary>
        public Vector3 WorldPos { get; }

        /// <summary> 网格坐标 </summary>
        public Vector3Int TilePos { get; } = new Vector3Int();

        /// <summary> 网格消耗 </summary>
        public int Cost { get; } = 0;

        /// <summary> 有单位会有阻挡（敌人会阻挡去路） </summary>
        public Func<Vector3Int, bool> IsWalkableFunc { get; } = (Vector3Int pos) => true;
        /// <summary> 是否可以作为终点（单位不能重合站，终点有单位）</summary>
        public Func<Vector3Int, bool> CanBeDustFunc { get; } = (Vector3Int pos) => true;

        /// <summary> 邻接的格子，在PathFinder初始化时计算 </summary>
        public List<XSTile> NearTileList { get; } = new List<XSTile>();

        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public XSTile(Vector3Int tilePos, Vector3 worldPos, int cost, Func<Vector3Int, bool> isWalkable = null, Func<Vector3Int, bool> canBeDustFunc = null)
        {
            this.TilePos = tilePos;
            this.WorldPos = worldPos;
            this.Cost = cost;

            if(isWalkable != null) 
                this.IsWalkableFunc = isWalkable;

            if(canBeDustFunc != null) 
                this.CanBeDustFunc = canBeDustFunc;
        }

        /// <summary> 返回1个默认值 </summary>
        static public XSTile Default()
        {
            return new XSTile(new Vector3Int(), new Vector3(), 0);
        }
    }
}