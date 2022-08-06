using System.Collections.Generic;

namespace XSSLG
{
    /// <summary>
    /// Implementation of A* pathfinding algorithm.
    /// </summary>
    class AStarPath
    {
        /// <summary> 查找返回一条路径 </summary>
        public List<XSTile> FindPath(XSTile src, XSTile dest)
        {
            var openQueue = new PriorityQueue<XSTile>();
            openQueue.Enqueue(src, 0);

            var aStarTileDict = new Dictionary<XSTile, AStarTile>();
            aStarTileDict.Add(src, new AStarTile(0, null));
            
            while (openQueue.Count != 0)
            {
                var current1 = openQueue.Dequeue();
                if (current1 == dest) 
                    break;

                current1.NearTileList.ForEach(tile => 
                {
                    var cost = aStarTileDict[current1].Cost + tile.Cost;
                    if (!aStarTileDict.ContainsKey(tile) || cost < aStarTileDict[tile].Cost)
                    {
                        aStarTileDict[tile] = new AStarTile(cost, current1);
                        openQueue.Enqueue(tile, cost + 1);
                    }
                });
            }

            var path = new List<XSTile>();
            if (!aStarTileDict.ContainsKey(dest))
                return path;

            var current = dest;
            while (current != src)
            {
                path.Add(current);
                current = aStarTileDict[current].PrevTile;
            }

            return path;
        }
    }
}