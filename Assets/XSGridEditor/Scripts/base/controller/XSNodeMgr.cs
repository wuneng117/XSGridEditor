using System.Collections.Generic;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSINodeMgr<T>
    {
        bool Add(T node);
        bool Remove(Vector3 worldPos);
    }

    /// <summary>  </summary>
    public interface XSINode
    {
        Vector3 WorldPos { get; set; }
        void RemoveNode();
        /// <summary> 不能直接用==取判断null，因为unity里的Object重载了==， 但是转成XSIBrushItem类取判断的时候是不会用重载了的==</summary>
        bool IsNull();
    }



    /// <summary>  </summary>
    public class XSNodeMgr<T> : XSINodeMgr<T> where T : class, XSINode
    {
        /************************* 变量 begin ***********************/
        public Dictionary<Vector3Int, T> Dict { get; private set; } = new Dictionary<Vector3Int, T>();

        /************************* 变量  end  ***********************/

        public virtual void CreateDict(List<T> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0)
            {
                return;
            }

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
            if (node == null || node.IsNull())
            {
                return false;
            }

            if (this.GetTile(node.WorldPos, out var tilePos))
            {
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
            if (this.GetTile(worldPos, out var tilePos, out var node))
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

        internal virtual bool GetTile(Vector3 worldPos, out Vector3Int tilePos) => this.GetTile(worldPos, out tilePos, out var node);

        internal virtual bool GetTile(Vector3 worldPos, out Vector3Int tilePos, out T node)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            tilePos = gridMgr.WorldToTile(worldPos);
            if (this.Dict.TryGetValue(tilePos, out node))
            {
                if (node == null || node.IsNull())
                {
                    this.Dict.Remove(tilePos);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}