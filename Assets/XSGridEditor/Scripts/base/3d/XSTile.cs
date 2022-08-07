/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成XSTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    using TileFunc = Func<Vector3Int, bool>;

    /// <summary> 用于计算的结构 </summary>
    public class XSTile
    {
        /// <summary> 世界坐标 </summary>
        protected Vector3 worldPos;
        public Vector3 WorldPos
        {
            get => worldPos;
            set
            {
                this.worldPos = value;
                if (UnityUtils.IsEditor())
                {
                    var dataEdit = this.Node?.GetComponent<XSTileDataEditMode>();
                    if (dataEdit)
                        dataEdit.PrevPos = this.Node.transform.localPosition;
                }
            }
        }

        /// <summary> 网格坐标 </summary>
        public Vector3Int TilePos { get; } = new Vector3Int();

        /// <summary> 网格消耗 </summary>
        public int Cost { get; } = 0;

        /// <summary> 对应节点 </summary>
        public XSTileData Node { get; } = null;

        /// <summary> 有单位会有阻挡（敌人会阻挡去路） </summary>
        public TileFunc IsWalkableFunc { get; } = (Vector3Int pos) => true;
        /// <summary> 是否可以作为终点（单位不能重合站，终点有单位）</summary>
        public TileFunc CanBeDustFunc { get; } = (Vector3Int pos) => true;

        /// <summary> 邻接的格子，在PathFinder初始化时计算 </summary>
        public List<XSTile> NearTileList { get; } = new List<XSTile>();

        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public XSTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSTileData node, TileFunc isWalkable = null, TileFunc canBeDustFunc = null)
        {
            this.TilePos = tilePos;
            this.WorldPos = worldPos;
            this.Cost = cost;
            this.Node = node;

            if (isWalkable != null)
                this.IsWalkableFunc = isWalkable;

            if (canBeDustFunc != null)
                this.CanBeDustFunc = canBeDustFunc;
        }

        /// <summary> 返回1个默认值 </summary>
        static public XSTile Default() => new XSTile(new Vector3Int(), new Vector3(), 0, null);
    }
}