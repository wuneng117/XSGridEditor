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

        /// <summary> 获取所有 XSTileData 节点 </summary>
        public List<XSTileData> GetTileDataList()=> this.TileRoot.GetComponentsInChildren<XSTileData>().ToList();

        /// <summary> 获取所有 XSObjectData 节点 </summary>
        public List<XSUnitData> GetUnitDataList()=> this.UnitRoot.GetComponentsInChildren<XSUnitData>().ToList();

        public Bounds GetBounds()
        {
            var ret = new Bounds();
            if (UnityUtils.IsEditor())
                return ret;

            var tiles = this.GetTileDataList();
            if (tiles.Count == 0)
                return ret;

            var collider = tiles[0].GetComponent<BoxCollider>();
            if (collider == null)
                return ret;

            var bound = collider.bounds;
            tiles.ForEach(tile =>
            {
                var col = tile.GetComponent<BoxCollider>();
                if (col)
                    bound.Encapsulate(col.bounds);
            });
            
            return bound;
        }
    }
}