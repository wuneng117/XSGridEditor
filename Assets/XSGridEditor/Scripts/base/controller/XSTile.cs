/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成XSTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;
using Mathf = UnityEngine.Mathf;

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
                if (XSUnityUtils.IsEditor())
                {
                    this.Node?.UpdateEditModePrevPos();
                }
            }
        }

        /// <summary> 网格坐标 </summary>
        public Vector3Int TilePos { get; } = new Vector3Int();

        /// <summary> 网格消耗 </summary>
        public int Cost { get; }

        /// <summary> 对应节点 </summary>
        public XSITileNode Node { get; }

        /// <summary> 有单位会有阻挡（敌人会阻挡去路） </summary>
        public TileFunc IsWalkableFunc { get; } = (tilePos) => true;
        /// <summary> 是否可以作为终点（单位不能重合站，终点有单位）</summary>
        public TileFunc CanBeDustFunc { get; } = (tilePos) => true;

        /// <summary> 邻接的格子，在PathFinder初始化时计算 </summary>
        public List<XSTile> NearTileList { get; } = new List<XSTile>();

        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public XSTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node)
        {
            this.TilePos = tilePos;
            this.WorldPos = worldPos;
            this.Cost = cost;
            this.Node = node;
        }

        public XSTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable) : this(tilePos, worldPos, cost, node)
        {
            if (isWalkable != null)
            {
                this.IsWalkableFunc = isWalkable;
            }
        }

        public XSTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable, TileFunc canBeDustFunc) : this(tilePos, worldPos, cost, node, isWalkable)
        {
            if (canBeDustFunc != null)
            {
                this.CanBeDustFunc = canBeDustFunc;
            }
        }
        
        /// <summary> 返回1个默认值 </summary>
        static public XSTile Default() => new XSTile(new Vector3Int(), new Vector3(), 0, null);

        public virtual bool PassNearRule(XSTile tile, int tileOffYMax) => Mathf.Abs(tile.WorldPos.y -this.WorldPos.y) < tileOffYMax;
    }
}