/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile 管理类，负责tile 坐标转化，数据等功能
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 管理类，负责tile 坐标转化，数据等功能 </summary>
    public class GridMgr : GridMgrBase
    {
        /// <summary> tile 大小，用来计算 tilePos </summary>
        private int TileSize { set; get; } = 1;

        public override Vector3 TileToWorld(Vector3Int tilePos)
        {
            var ret = new Vector3(0, 0, 0);

            if (tilePos.x < 0 || tilePos.y < 0)
                return ret;

            tilePos.z = 0;
            var tile = this.GetTile(tilePos);
            if (tile == null) return ret;

            return tile.WorldPos;
        }
        
        public override Vector3Int WorldToTile(Vector3 worldPos)
        {
            var ret = new Vector3Int(-1, -1, 0);
            ret.x = Mathf.FloorToInt((worldPos.x) / this.TileSize);
            ret.y = Mathf.FloorToInt((worldPos.z) / this.TileSize);
            return ret;
        }

        protected override Dictionary<Vector3Int, PathFinderTile> CreatePathFinderTileDict()
        {
            var ret = new Dictionary<Vector3Int, PathFinderTile>();
            var gridHelper = Component.FindObjectOfType<XSGridHelper>();
            if (gridHelper == null)
                return ret;

            var tileDataList = gridHelper.GetTileDataArray();
            if (tileDataList == null)
                return ret;
            
            var tileData = tileDataList.First();
            this.TileSize = Mathf.FloorToInt(tileData.gameObject.transform.localScale.x);

            // 遍历Tile
            tileDataList.ToList().ForEach(tile =>
            {
                var tilePos = this.WorldToTile(tile.transform.position);
                var pathFinderTile = new PathFinderTile(tilePos, tile.transform.position, tile.Cost);
                ret.Add(tilePos, pathFinderTile);
                tile.Tile = pathFinderTile;
            });

            this.GetBounds();
            return ret;
        }

        public Bounds GetBounds()
        {
            var gridHelper = Component.FindObjectOfType<XSGridHelper>();
            var tileDataList = gridHelper.GetTileDataArray();
            var bound = tileDataList[0].GetComponent<BoxCollider>().bounds;
            foreach (var mCollider in tileDataList)
                bound.Encapsulate(mCollider.GetComponent<BoxCollider>().bounds);
            return bound;
        }
    }
}