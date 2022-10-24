using System.Collections.Generic;
using System.Linq;

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
        public virtual Dictionary<XSTile, List<XSTile>> FindPath(XSTile src, XSTile dest, int totalCost)
        {
            var openQueue = new PriorityQueue<XSTile>();
            openQueue.Enqueue(src, 0);

            var aStarTileDict = new Dictionary<XSTile, XSAStarTile>();
            aStarTileDict.Add(src, new XSAStarTile(0, null));

            while (openQueue.Count != 0)
            {
                var current = openQueue.Dequeue();
                
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
                        var manhattanDistance = Mathf.Abs(dest.x - tile.x) + Mathf.Abs(dest.z - tile.z);
                        aStarTileDict[tile] = new XSAStarTile(cost, manhattanDistance, current);
                        openQueue.Enqueue(tile, manhattanDistance);
                    }
                });
            }

            var paths = new Dictionary<XSTile, List<XSTile>>();
            aStarTileDict.Keys.ToList().ForEach(tile =>
            {
                List<XSTile> path = new List<XSTile>();
                var current = tile;
                while (current != src)
                {
                    path.Add(current);
                    current = aStarTileDict[current].PrevTile;
                }
                paths.Add(tile, path);
            });
            return paths;
        }
    }
}