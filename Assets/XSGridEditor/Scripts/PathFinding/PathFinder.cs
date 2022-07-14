/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成PathFInderTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 用于计算的结构 </summary>
    public class PathFinderTile
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
        public List<PathFinderTile> NearTileList { get; } = new List<PathFinderTile>();

        // public PathFinderTile(Vector3 worldPos, Vector3Int tilePos, int cost)
        public PathFinderTile(Vector3Int tilePos, Vector3 worldPos, int cost, Func<Vector3Int, bool> isWalkable = null, Func<Vector3Int, bool> canBeDustFunc = null)
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
        static public PathFinderTile Default()
        {
            return new PathFinderTile(new Vector3Int(), new Vector3(), 0);
        }
    }

    /// <summary> 寻路 </summary>
    public class PathFinder
    {
        /// <summary> 四边形地图链接格子就是这4个位置偏移 </summary>
        public static readonly Vector3Int[] NearPosArray = { new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), };

        /// <summary> 以tilepos为key存储所有tile。 </summary>
        public Dictionary<Vector3Int, PathFinderTile> TileDict { get; } = new Dictionary<Vector3Int, PathFinderTile>();

        /// <summary> 以tilepos为key更新TileDict。 </summary>
        public void UpdateTileDict(PathFinderTile tile)
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

        /// <summary> 寻路插件 </summary>
        private static IPathfinding _fallbackPathfinder = new AStarPathfinding();

        /// <summary> 寻路插件，可以用来显示所有的路径 </summary>
        private static DijkstraPathfinding _pathfinder = new DijkstraPathfinding();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tileList">tile字典，key是tile对应的网格坐标</param>
        public PathFinder(Dictionary<Vector3Int, PathFinderTile> tileDict)
        {
            this.TileDict = tileDict;
            
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

        /// <summary>
        /// 寻路函数
        /// </summary>
        /// <param name="unit">要寻路的unit</param>
        /// <param name="srcTile">unit所在的 tile </param>
        /// <param name="destTile">目的地 tile </param>
        /// <param name="isIgnoreDest">忽略dest的unit，用于计算AI走到玩家unit的路径计算</param>
        /// <returns></returns>
        public List<Vector3Int> FindPath(PathFinderTile srcTile, PathFinderTile destTile)
        {
            if (srcTile == destTile)
            {
                var ret = new List<Vector3Int>();
                // ret.Add(dest.TilePos);
                ret.Add(destTile.TilePos);
                return ret;
            }

            var edges = PathFinder.GetGraphEdges(this.TileDict.Values.ToList());
            var pathTileList = _fallbackPathfinder.FindPath(edges, srcTile, destTile);
            return pathTileList.Select(pathTile => pathTile.TilePos).ToList();
        }

        /// <summary>
        /// 返回所有的路径
        /// </summary>
        /// <param name="unit">要寻路的unit</param>
        /// <param name="srcWorldPos">unit所在的worldpos</param>
        /// <param name="moveRange">移动范围，默认-1和小于0都表示不限制移动范围</param>
        /// <returns></returns>
        public Dictionary<Vector3Int, List<Vector3Int>> FindAllPath(PathFinderTile srcTile, int moveRange)
        {
            if (srcTile == null)
                return new Dictionary<Vector3Int, List<Vector3Int>>();

            var edges = PathFinder.GetGraphEdges(this.TileDict.Values.ToList());

            var allPaths = _pathfinder.FindAllPaths(edges, srcTile, moveRange);
            //移动到原地的路径长度为0，为了不和没有路径搞混，原地网格加进去
            allPaths[srcTile].Add(srcTile);
            // 把PathFinderTile全部转为对应的TilePos，并且过滤掉不能作为终点的路径
            var allTilePosPaths = allPaths.Where(path => path.Key.CanBeDustFunc == null || path.Key.CanBeDustFunc(path.Key.TilePos))
                                          .ToDictionary(pair => pair.Key.TilePos, pair => pair.Value.Select(pathTile => pathTile.TilePos).ToList());
            return allTilePosPaths;
        }

        /// <summary>
        /// 返回一个tile作为key，value的key是他链接的格子，value的value是格子的移动损耗
        /// </summary>
        protected static Dictionary<PathFinderTile, Dictionary<PathFinderTile, float>> GetGraphEdges(List<PathFinderTile> pathTileList)
        {
            var ret = new Dictionary<PathFinderTile, Dictionary<PathFinderTile, float>>();

            pathTileList.ForEach(pathTile =>
            {
                // 这个tile上敌人，不能往上面走过
                if (pathTile.IsWalkableFunc != null && !pathTile.IsWalkableFunc(pathTile.TilePos))
                    return;

                ret[pathTile] = new Dictionary<PathFinderTile, float>();
                foreach (var nearTile in pathTile.NearTileList)
                {
                    // 这个tile上敌人，不能往上面走过
                    if (pathTile.IsWalkableFunc != null && !pathTile.IsWalkableFunc(pathTile.TilePos))
                        continue;

                    ret[pathTile][nearTile] = nearTile.Cost;
                }
            });
            return ret;
        }
    }
}