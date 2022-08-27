using System.Collections.Generic;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    /// <summary>  </summary>
    public interface XSIBrushItem
    {
        Vector3 WorldPos { get; set; }
        void RemoveNode();
    }

    /// <summary>  </summary>
    public class XSBrushItemMgr<T> where T : XSIBrushItem
    {
        /************************* 变量 begin ***********************/
        // private Transform UnitRoot { get; }
        public Dictionary<Vector3Int, T> Dict { get; private set; } = new Dictionary<Vector3Int, T>();

        /************************* 变量  end  ***********************/
        // public XSGridItemMgr(XSGridHelper helper)
        // {
        // }

        public virtual void CreateDict(List<T> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0)
                return;

            // 遍历
            nodeList.ForEach(node => this.Add(node));
        }

        /// <summary>
        /// 添加到字典中
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool Add(T node)
        {
            if (node == null)
                return false;

            Vector3Int tilePos;
            if (this.GetTilePos(node.WorldPos, out tilePos))
            {
                // Debug.LogWarning("XSBaseMgr.Add: 同一tilePos上已经存在unitData：" + tilePos);
                return false;
            }
            else
            {
                this.Dict.Add(tilePos, node);
                return true;
            }
        }

        public virtual bool Remove(Vector3 worldPos)
        {
            Vector3Int tilePos;
            if (this.GetTilePos(worldPos, out tilePos))
            {
                var node = this.Dict[tilePos];
                node.RemoveNode();
                this.Dict.Remove(tilePos);
                return true;
            }
            else
            {
                // Debug.Log("XSBaseMgr.RemoveXSUnit: 这个位置上不存在unit，tilePos：" + tilePos);
                return false;
            }
        }

        protected virtual bool GetTilePos(Vector3 worldPos, out Vector3Int tilePos)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            tilePos = gridMgr.WorldToTile(worldPos);
            if (this.Dict.ContainsKey(tilePos))
                return true;
            else
                return false;
        }

        public virtual T GetItem(Vector3 worldPos)
        {
            Vector3Int tilePos;
            if (this.GetTilePos(worldPos, out tilePos))
                return this.Dict[tilePos];
            else
                return default(T);
        }
    }
}