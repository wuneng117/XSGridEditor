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
using Quaternion = UnityEngine.Quaternion;

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

        //<summary> 可通行性 </summary>  
        public Accessibility Access { get; }

        /// <summary> 对应节点 </summary>
        public XSITileNode Node { get; }

        /// <summary> 有单位会有阻挡（敌人会阻挡去路） </summary>
        public TileFunc IsWalkableFunc { get; protected set; } = (tilePos) => true;
        /// <summary> 是否可以作为终点（单位不能重合站，终点有单位）</summary>
        public TileFunc CanBeDustFunc { get; protected set; } = (tilePos) => true;

        /// <summary> 邻接的格子，在PathFinder初始化时计算 </summary>
        public List<XSTile> NearTileList { get; } = new List<XSTile>();

        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public XSTile(Vector3Int tilePos, XSITileNode node)
        {
            this.TilePos = tilePos;
            this.Node = node;
            if (node != null)
            {
                this.WorldPos = node.WorldPos;
                this.Cost = node.Cost;
                this.Access = node.Access;
            }

        }

        public XSTile(Vector3Int tilePos, XSITileNode node, TileFunc isWalkable) : this(tilePos, node)
        {
            if (isWalkable != null)
            {
                this.IsWalkableFunc = isWalkable;
            }
        }

        public XSTile(Vector3Int tilePos, XSITileNode node, TileFunc isWalkable, TileFunc canBeDustFunc) : this(tilePos,  node, isWalkable)
        {
            if (canBeDustFunc != null)
            {
                this.CanBeDustFunc = canBeDustFunc;
            }
        }

        /// <summary> 返回1个默认值 </summary>
        static public XSTile Default() => new XSTile(new Vector3Int(), null);

        public virtual bool PassNearRule(XSTile tile, Vector3Int direct, int tileOffYMax)
        {

            var tempDirect = Quaternion.Euler(0, -this.Node.AngleY, 0) * new Vector3(direct.x, direct.y, direct.z);
            direct = new Vector3Int(Mathf.RoundToInt(tempDirect.x), Mathf.RoundToInt(tempDirect.y), Mathf.RoundToInt(tempDirect.z));
            if ((direct == Vector3Int.left && !this.Node.Access.Left) ||
                (direct == Vector3Int.right && !this.Node.Access.Right) ||
                (direct == Vector3Int.back && !this.Node.Access.Down) ||
                (direct == Vector3Int.forward && !this.Node.Access.Up))
            {
                return false;
            }

            return Mathf.Abs(tile.WorldPos.y - this.WorldPos.y) < tileOffYMax;
        }
    }
}