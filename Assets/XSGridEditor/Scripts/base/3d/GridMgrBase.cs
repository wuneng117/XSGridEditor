/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile 管理类基类，负责tile 坐标转化，数据等功能
/// </summary>
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 管理类基类，负责tile 坐标转化，数据等功能 </summary>
    public abstract class GridMgrBase : IGridMgr
    {
        /// <summary> 四边形地图链接格子就是这4个位置偏移 </summary>
        public static readonly Vector3Int[] NearPosArray = { new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), };

        /// <summary> 以tilepos为key存储所有tile。 </summary>
        public Dictionary<Vector3Int, XSTile> TileDict { get; } = new Dictionary<Vector3Int, XSTile>();

        public GridMgrBase()
        {
            this.TileDict = this.CreatePathFinderTileDict();

            // 为每个PathFinderTile计算它的链接格子
            foreach (var pair in this.TileDict)
            {
                foreach (var pos in NearPosArray)
                {
                    var nearPos = pair.Key + pos;
                    if (this.TileDict.ContainsKey(nearPos))
                        pair.Value.NearTileList.Add(this.TileDict[nearPos]);
                }
            }
        }

        /// <summary> 以tilepos为key更新TileDict。 </summary>
        public void UpdateTileDict(XSTile tile)
        {
            if (this.TileDict.ContainsKey(tile.TilePos))
            {
                this.TileDict[tile.TilePos] = tile;
            }
            else
            {
                this.TileDict.Add(tile.TilePos, tile);
            }

        }

        public abstract Vector3 TileToWorld(Vector3Int tilePos);
        public abstract Vector3Int WorldToTile(Vector3 worldPos);

        //1game
        public XSTile GetTile(Vector3 worldPos)
        {
            var tilelPos = this.WorldToTile(worldPos);
            return this.GetTile(tilelPos);
        }

        public XSTile GetTile(Vector3Int tilePos)
        {
            if (this.TileDict.ContainsKey(tilePos))
                return this.TileDict[tilePos];
            else
                return null;
        }

        public Vector3 WorldToTileCenterWorld(Vector3 worldPos)
        {
            var cellPos = this.WorldToTile(worldPos);
            var ret = this.TileToWorld(cellPos);
            return ret;
        }

        /// <summary>
        /// 创建一个以 tilepos 为 key，PathFinderTile 为 value 的 Dictionary，用来生成 PathFinder
        /// </summary>
        /// <returns>以 tilepos 为 key，PathFinderTile 为 value 的 Dictionary </returns>
        protected abstract Dictionary<Vector3Int, XSTile> CreatePathFinderTileDict();

        /// <summary>
        /// 寻路函数
        /// </summary>
        /// <param name="srcTile">起点tile</param>
        /// <param name="destTile">目的地 tile </param>
        /// <returns></returns>
        public List<Vector3Int> FindPath(XSTile srcTile, XSTile destTile) => PathFinder.FindPath(this.TileDict, srcTile, destTile);

        /// <summary>
        /// 返回所有的路径
        /// </summary>
        /// <param name="srcTile">起点tile</param>
        /// <param name="moveRange">移动范围，默认-1和小于0都表示不限制移动范围</param>
        /// <returns></returns>
        public Dictionary<Vector3Int, List<Vector3Int>> FindAllPath(XSTile srcTile, int moveRange) => PathFinder.FindAllPath(this.TileDict, srcTile, moveRange);
    }
}