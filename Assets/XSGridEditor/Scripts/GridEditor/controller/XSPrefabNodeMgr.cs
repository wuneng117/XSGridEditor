
using System.Collections.Generic;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;
using UnityEditor.SceneManagement;
using System.Linq;

namespace XSSLG
{
    public class XSPrefabNodeMgr : XSINodeMgr<XSPrefabNode>
    {
        /************************* variable begin ***********************/
        public Dictionary<Vector3Int, List<XSPrefabNode>> Dict { get; private set; } = new Dictionary<Vector3Int, List<XSPrefabNode>>();

        /************************* variable  end  ***********************/
        public XSPrefabNodeMgr()
        {
            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var prefabList = currentStageHandle.FindComponentsOfType<XSPrefabNode>().ToList();
            this.CreateDict(prefabList);
        }

        public virtual void CreateDict(List<XSPrefabNode> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0)
            {
                return;
            }

            // traverse
            nodeList.ForEach(node => this.Add(node));
        }

        /// <summary>
        /// add to dict
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool Add(XSPrefabNode node)
        {
            if (node == null || node.IsNull())
            {
                return false;
            }

            var list = this.GetOrCreateList(node.WorldPos, out var tilePos);
            list.Add(node);
            this.CheckList(list);

            return true;
        }

        public virtual bool Remove(Vector3 worldPos)
        {
            var list = this.GetOrCreateList(worldPos);
            if (list.Count > 0)
            {
                var node = list[list.Count - 1];
                node.RemoveNode();
                list.RemoveAt(list.Count - 1);
                this.CheckList(list);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool HasItem(Vector3 worldPos)
        {
            var list = this.GetOrCreateList(worldPos);
            return list.Count > 0;
        }

        internal virtual List<XSPrefabNode> GetOrCreateList(Vector3 worldPos) => this.GetOrCreateList(worldPos, out var tilePos);

        internal virtual List<XSPrefabNode> GetOrCreateList(Vector3 worldPos, out Vector3Int tilePos)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            tilePos = gridMgr.WorldToTile(worldPos);
            if(!this.Dict.TryGetValue(tilePos, out var list))
            {
                list = new List<XSPrefabNode>();
                this.Dict.Add(tilePos, list);
            }

            return list;
        }

        protected void CheckList(List<XSPrefabNode> list)
        {
            list.RemoveAll(node => node == null || node.IsNull());
            list.Sort((a, b) => (int)(a.WorldPos.y - b.WorldPos.y));
        }
    }
}