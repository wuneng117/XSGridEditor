/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: this is a square tile, convert all XSTileNode into XSTile, and then we can use pathfinding
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    using TileDict = Dictionary<Vector3Int, XSTile>;
    using PathsDict = Dictionary<Vector3, List<Vector3>>;
    
    public class XSPathFinder
    {
        protected XSPathFinder() {}

        // /// <summary> Pathfinding plugin that can be used to display all paths </summary>
        protected static readonly XSDijkstraPath _dijkstraPath = new XSDijkstraPath();

        /// <summary>
        /// get all paths
        /// </summary>
        /// <param name="srcTile">beginning tile</param>
        /// <param name="moveRange"> -1 or less than 0 means no limit to move range </param>
        /// <returns></returns>
        public static PathsDict FindAllPath(XSTile srcTile, int moveRange)
        {
            if (srcTile == null)
            {
                return new PathsDict();
            }

            var allPaths = _dijkstraPath.FindAllPaths(srcTile, moveRange);
            // The length of the path moving to the original place is 0. In order not to be confused with no path, the original grid is added.
            if (allPaths[srcTile] == null)
            {
                allPaths[srcTile] = new List<XSTile>();
            }

            allPaths[srcTile].Add(srcTile);
            // Convert all XSTileNode to corresponding tile position, and filter out paths that cannot be used as end points
            var allTilePosPaths = allPaths.Where(path => path.Key.CanBeDustFunc == null || path.Key.CanBeDustFunc(path.Key.TilePos))
                                          .ToDictionary(pair => pair.Key.WorldPos, pair => pair.Value.Select(pathTile => pathTile.WorldPos).ToList());
            return allTilePosPaths;
        }
    }
}