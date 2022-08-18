using System.Collections.Generic;
using System.Linq;

namespace XSSLG
{
    class XSDijkstraPath
    {
        /// <summary>
        /// 查找返回所有可能的路径
        /// </summary>
        /// <param name="src"></param>
        /// <param name="totalCost">总消耗，相当于移动范围差不多，默认-1和小于0都表示不限制</param>
        /// <returns></returns>
        public Dictionary<XSTile, List<XSTile>> FindAllPaths(XSTile src, int totalCost)
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
                    
                    // 必须小于总消耗
                    if (totalCost >= 0 && cost > totalCost)
                        return;

                    if (!aStarTileDict.ContainsKey(tile) || cost < aStarTileDict[tile].Cost)
                    {
                        aStarTileDict[tile] = new XSAStarTile(cost, current);
                        openQueue.Enqueue(tile, cost);
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