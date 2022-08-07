/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成XSTile，再调用寻路
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    using TileDict = Dictionary<Vector3Int, XSTile>;
    using PathsDict = Dictionary<Vector3, List<Vector3>>;
    
    /// <summary> 寻路 </summary>
    public class PathFinder
    {
        /// <summary> 寻路插件 </summary>
        private static AStarPath _aStarPath = new AStarPath();

        // /// <summary> 寻路插件，可以用来显示所有的路径 </summary>
        private static DijkstraPath _dijkstraPath = new DijkstraPath();

        /// <summary>
        /// 寻路函数
        /// </summary>
        /// <param name="TileDict">所有tile的字典</param>
        /// <param name="srcTile">起点tile</param>
        /// <param name="destTile">目的地 tile </param>
        /// <returns></returns>
        public static List<Vector3> FindPath(TileDict TileDict, XSTile srcTile, XSTile destTile)
        {
            if (srcTile == destTile)
            {
                var ret = new List<Vector3>();
                ret.Add(destTile.TilePos);
                return ret;
            }

            var pathTileList = _aStarPath.FindPath(srcTile, destTile);
            return pathTileList.Select(pathTile => pathTile.WorldPos).ToList();
        }

        /// <summary>
        /// 返回所有的路径
        /// </summary>
        /// <param name="TileDict">所有tile的字典</param>
        /// <param name="srcTile">起点tile</param>
        /// <param name="moveRange">移动范围，默认-1和小于0都表示不限制移动范围</param>
        /// <returns></returns>
        public static PathsDict FindAllPath(TileDict TileDict, XSTile srcTile, int moveRange)
        {
            if (srcTile == null)
                return new PathsDict();

            var allPaths = _dijkstraPath.FindAllPaths(srcTile, moveRange);
            //移动到原地的路径长度为0，为了不和没有路径搞混，原地网格加进去
            if (allPaths[srcTile] == null)
                allPaths[srcTile] = new List<XSTile>();
            allPaths[srcTile].Add(srcTile);
            // 把PathFinderTile全部转为对应的TilePos，并且过滤掉不能作为终点的路径
            var allTilePosPaths = allPaths.Where(path => path.Key.CanBeDustFunc == null || path.Key.CanBeDustFunc(path.Key.TilePos))
                                          .ToDictionary(pair => pair.Key.WorldPos, pair => pair.Value.Select(pathTile => pathTile.WorldPos).ToList());
            return allTilePosPaths;
        }
    }
}