/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: this is a square tile, convert all XSTileNode into XSTile, and then we can use pathfinding
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

    /// <summary> tile struct to caculate path </summary>
    public class XSTile
    {
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

        public Vector3Int TilePos { get; } = new Vector3Int();

        /// <summary> tile move cost </summary>
        public int Cost { get; }

        public Accessibility Access { get; }

        /// <summary> this node in scene </summary>
        public XSITileNode Node { get; }

        /// <summary> the tile can walkable (the enemy will block the way) </summary>
        public TileFunc IsWalkableFunc { get; protected set; } = (tilePos) => true;
        /// <summary> Whether this tile can be the end of path (two units cannot in the same tile)</summary>
        public TileFunc CanBeDustFunc { get; protected set; } = (tilePos) => true;

        public List<XSTile> NearTileList { get; } = new List<XSTile>();

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

        /// <summary> return default value </summary>
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