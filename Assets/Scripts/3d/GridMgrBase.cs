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
        public PathFinder PathFinder { get; }

        public GridMgrBase()
        {
            this.PathFinder = new PathFinder(this.CreatePathFinderTileDict());
        }

        public abstract Vector3 TileToWorld(Vector3Int tilePos);
        public abstract Vector3Int WorldToTile(Vector3 worldPos);

        public PathFinderTile GetTile(Vector3 worldPos)
        {
            var tilelPos = this.WorldToTile(worldPos);
            return this.GetTile(tilelPos);
        }

        public PathFinderTile GetTile(Vector3Int tilePos)
        {
            if (this.PathFinder.TileDict.ContainsKey(tilePos))
                return this.PathFinder.TileDict[tilePos];
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
        protected abstract Dictionary<Vector3Int, PathFinderTile> CreatePathFinderTileDict();
    }
}