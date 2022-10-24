using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    public class XSAStarPath
    {
        /// <summary>
        /// get all paths
        /// </summary>
        /// <param name="src"></param>
        /// <param name="totalCost"> like move cost, -1 or less than 0 means no limit to move range </param>
        /// <returns></returns>
        public virtual List<XSTile> FindPath(XSTile src, XSTile dest, int totalCost)
        {
            var openQueue = new PriorityQueue<XSTile>();
            openQueue.Enqueue(src, 0);

            var aStarTileDict = new Dictionary<XSTile, XSAStarTile>();
            var manhattanDistance = Mathf.Abs(dest.TilePos.x - src.TilePos.x) + Mathf.Abs(dest.TilePos.z - src.TilePos.z);
            aStarTileDict.Add(src, new XSAStarTile(0, manhattanDistance, null));

            while (openQueue.Count != 0)
            {
                var current = openQueue.Dequeue();
                if (current == dest) break;
                
                current.NearTileList.ForEach(tile => 
                {
                    var cost = aStarTileDict[current].Cost + tile.Cost;
                    
                    // cost must less then total cost
                    if ((totalCost >= 0 && cost > totalCost) ||!src.IsWalkable())
                    {
                        return;
                    }

                    if (!aStarTileDict.ContainsKey(tile) || cost < aStarTileDict[tile].Cost)
                    {
                        var manhattanDistance = Mathf.Abs(dest.TilePos.x - tile.TilePos.x) + Mathf.Abs(dest.TilePos.z - tile.TilePos.z);
                        aStarTileDict[tile] = new XSAStarTile(cost, manhattanDistance, current);
                        openQueue.Enqueue(tile, manhattanDistance);
                    }
                });
            }

            List<XSTile> path = new List<XSTile>();
            var tile = dest;
            while (tile != null && tile != src)
            {
                path.Add(tile);
                tile = aStarTileDict[tile].PrevTile;
            }
            return path;
        }
    }
}