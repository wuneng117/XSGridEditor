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
    public class XSLadderTile : XSTile
    {
        protected XSGridDefine.XSLadderTileType Type { get; }


        public XSLadderTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, XSGridDefine.XSLadderTileType type) : base(tilePos, worldPos, cost, node)
        {
            this.Type = type;
        }

        public XSLadderTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable, XSGridDefine.XSLadderTileType type) : this(tilePos, worldPos, cost, node, type)
        {
            if (isWalkable != null)
            {
                this.IsWalkableFunc = isWalkable;
            }
        }

        public XSLadderTile(Vector3Int tilePos, Vector3 worldPos, int cost, XSITileNode node, TileFunc isWalkable, TileFunc canBeDustFunc, XSGridDefine.XSLadderTileType type) : this(tilePos, worldPos, cost, node, isWalkable, type)
        {
            if (canBeDustFunc != null)
            {
                this.CanBeDustFunc = canBeDustFunc;
            }
        }

        public override bool PassNearRule(XSTile tile, int tileOffYMax)
        {
            if (this.GetType() != tile.GetType())
            {
                switch (this.Type)
                {
                    case XSGridDefine.XSLadderTileType.UpDown:
                        {
                            if (tile.TilePos.x != this.TilePos.x)
                            {
                                return false;
                            }
                            break;
                        }
                    case XSGridDefine.XSLadderTileType.LeftRight:
                        {
                            if (tile.TilePos.z != this.TilePos.z)
                            {
                                return false;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }

            return base.PassNearRule(tile, tileOffYMax);
        }
    }
}