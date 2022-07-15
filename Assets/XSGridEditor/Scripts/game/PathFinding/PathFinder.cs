/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成PathFInderTile，再调用寻路
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 寻路 </summary>
    public class PathFinder
    {
        /// <summary> 寻路插件 </summary>
        private static IPathfinding _fallbackPathfinder = new AStarPathfinding();

        /// <summary> 寻路插件，可以用来显示所有的路径 </summary>
        private static DijkstraPathfinding _pathfinder = new DijkstraPathfinding();


        /// <summary>
        /// 寻路函数
        /// </summary>
        /// <param name="TileDict">所有tile的字典</param>
        /// <param name="srcTile">起点tile</param>
        /// <param name="destTile">目的地 tile </param>
        /// <returns></returns>
        public static List<Vector3Int> FindPath(Dictionary<Vector3Int, XSTile> TileDict, XSTile srcTile, XSTile destTile)
        {
            if (srcTile == destTile)
            {
                var ret = new List<Vector3Int>();
                // ret.Add(dest.TilePos);
                ret.Add(destTile.TilePos);
                return ret;
            }

            var edges = PathFinder.GetGraphEdges(TileDict.Values.ToList());
            var pathTileList = _fallbackPathfinder.FindPath(edges, srcTile, destTile);
            return pathTileList.Select(pathTile => pathTile.TilePos).ToList();
        }

        /// <summary>
        /// 返回所有的路径
        /// </summary>
        /// <param name="TileDict">所有tile的字典</param>
        /// <param name="srcTile">起点tile</param>
        /// <param name="moveRange">移动范围，默认-1和小于0都表示不限制移动范围</param>
        /// <returns></returns>
        public static Dictionary<Vector3Int, List<Vector3Int>> FindAllPath(Dictionary<Vector3Int, XSTile> TileDict, XSTile srcTile, int moveRange)
        {
            if (srcTile == null)
                return new Dictionary<Vector3Int, List<Vector3Int>>();

            var edges = PathFinder.GetGraphEdges(TileDict.Values.ToList());

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
        protected static Dictionary<XSTile, Dictionary<XSTile, float>> GetGraphEdges(List<XSTile> pathTileList)
        {
            var ret = new Dictionary<XSTile, Dictionary<XSTile, float>>();

            pathTileList.ForEach(pathTile =>
            {
                // 这个tile上敌人，不能往上面走过
                if (pathTile.IsWalkableFunc != null && !pathTile.IsWalkableFunc(pathTile.TilePos))
                    return;

                ret[pathTile] = new Dictionary<XSTile, float>();
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