using System.Collections.Generic;

namespace XSSLG
{
    /// <summary>
    /// Implementation of A* pathfinding algorithm.
    /// </summary>
    class AStarPathfinding
    {


        /// <summary> 查找返回一条路径 </summary>
        public List<XSTile> FindPath(XSTile src, XSTile dest)
        {
            IPriorityQueue<XSTile> frontier = new HeapPriorityQueue<XSTile>();
            frontier.Enqueue(src, 0);

            var aStarTileDict = new Dictionary<global::XSSLG.XSTile, AStarTile>();
            aStarTileDict.Add(src, new AStarTile(0, null));
            
            while (frontier.Count != 0)
            {
                var current = frontier.Dequeue();
                if (current.Equals(dest)) break;

                current.NearTileList.ForEach(tile => 
                {
                    var newCost = aStarTileDict[current].Cost + tile.Cost;
                    if (!aStarTileDict.ContainsKey(tile) || newCost < aStarTileDict[tile].Cost)
                    {
                        aStarTileDict[tile] = new AStarTile(newCost, current);
                        frontier.Enqueue(tile, newCost + 1);
                    }
                });
            }

            List<XSTile> path = new List<XSTile>();
            if (!aStarTileDict.ContainsKey(dest))
                return path;

            path.Add(dest);
            var temp = dest;

            while (aStarTileDict[temp].PrevTile != src)
            {
                var currentPathElement = aStarTileDict[temp].PrevTile;
                path.Add(currentPathElement);

                temp = currentPathElement;
            }

            return path;
        }
    }
}