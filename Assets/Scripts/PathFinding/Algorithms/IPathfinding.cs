using System.Collections.Generic;
using System.Linq;

namespace XSSLG
{
    public abstract class IPathfinding
    {
        public abstract List<T> FindPath<T>(Dictionary<T, Dictionary<T, float>> edges, T originNode, T destinationNode);

        protected List<T> GetNeigbours<T>(Dictionary<T, Dictionary<T, float>> edges, T node)
        {
            if (!edges.ContainsKey(node))
                return new List<T>();

            return edges[node].Keys.ToList();
        }
    }
}