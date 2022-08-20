/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 辅助类
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 辅助类 </summary>
    [ExecuteInEditMode]
    public class XSGridHelper : MonoBehaviour
    {
        /// <summary> tile根节点 </summary>
        public Transform TileRoot = null;

        /// <summary> unit根节点 </summary>
        public Transform UnitRoot = null;

        /// <summary> 移动范围用的 prefab </summary>
        public GameObject MoveTilePrefab = null;

        /// <summary> 获取所有 XSITileNode 节点 </summary>
        public List<XSITileNode> GetTileNodeList()=> this.TileRoot.GetComponentsInChildren<XSITileNode>().ToList();

        /// <summary> 获取所有 XSObjectData 节点 </summary>
        public List<XSIUnitNode> GetUnitDataList()=> this.UnitRoot.GetComponentsInChildren<XSIUnitNode>().ToList();

        public virtual Bounds GetBounds()
        {
            var ret = new Bounds();
            if (XSUnityUtils.IsEditor())
                return ret;

            var tiles = this.GetTileNodeList();
            if (tiles.Count == 0)
                return ret;

            var collider = ((XSTileNode)tiles[0]).GetComponent<BoxCollider>();
            if (collider == null)
                return ret;

            var bound = collider.bounds;
            tiles.ForEach(tile =>
            {
                var col = ((XSTileNode)tile).GetComponent<BoxCollider>();
                if (col)
                    bound.Encapsulate(col.bounds);
            });
            
            return bound;
        }
    }
}
