using System;
using System.Collections.Generic;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSINodeMgr<T>
    {
        bool Add(T node);
        bool RemoveByWorldPos(Vector3 worldPos);
    }

    public interface XSINode
    {
        Vector3 WorldPos { get; set; }
        void RemoveNode();
        /// <summary> 
        /// You cannot  use "==" to check gameobject is null, because the gameobject in Unity is override "==", 
        /// but when the gameobject is converted to the XSITileRoot, it will not use the override "==" 
        /// </summary>
        bool IsNull();
    }


    public class XSNodeMgr<T> : XSINodeMgr<T> where T : class, XSINode
    {
        /************************* variable begin ***********************/
        public Dictionary<Vector3Int, T> Dict { get; private set; } = new Dictionary<Vector3Int, T>();

        /************************* variable  end  ***********************/

        public virtual void CreateDict(List<T> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0)
            {
                return;
            }

            // traverse
            nodeList.ForEach(node => this.Add(node));
        }

        public virtual void ForEach(Action<T> action)
        {
            foreach (var item in this.Dict)
            {
                action(item.Value);
            }
        }

        /// <summary>
        /// added to dict
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool Add(T node)
        {
            if (node == null || node.IsNull())
            {
                return false;
            }

            if (this.HasByWorldPos(node.WorldPos))
            {
                return false;
            }
            else
            {
                var tilePos = XSU.GridMgr.WorldToTile(node.WorldPos);
                this.Dict.Add(tilePos, node);
                return true;
            }
        }

        public virtual bool Remove(T node)
        {
            var tilePos = XSU.GridMgr.WorldToTile(node.WorldPos);
            return this.Remove(tilePos);
        }

        public virtual bool Remove(Vector3Int tilePos)
        {
            if (this.TryGet(tilePos, out var node))
            {
                node.RemoveNode();
                this.Dict.Remove(tilePos);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool RemoveByWorldPos(Vector3 worldPos)
        {
            var tilePos = XSU.GridMgr.WorldToTile(worldPos);
            return this.Remove(tilePos);
        }

        public virtual bool Has(Vector3Int tilePos) => this.Dict.ContainsKey(tilePos);

        public virtual bool HasByWorldPos(Vector3 worldPos) => this.TryGetByWorldPos(worldPos, out var node);

        internal virtual T Get(Vector3Int tilePos)
        {
            this.TryGet(tilePos, out var node);
            return node;
        }

        internal virtual T GetByWorldPos(Vector3 worldPos)
        {
            this.TryGetByWorldPos(worldPos, out var node);
            return node;
        }

        public virtual bool TryGet(Vector3Int tilePos, out T node)
        {
            if (this.Dict.TryGetValue(tilePos, out node) && node != null && !node.IsNull())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool TryGetByWorldPos(Vector3 worldPos, out T node)
        {
            var tilePos = XSU.GridMgr.WorldToTile(worldPos);
            return this.TryGet(tilePos, out node);
        }

    }
}