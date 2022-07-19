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

            // if (tilePos.x < 0 || tilePos.y < 0)
            //     return ret;

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

        protected override Dictionary<Vector3Int, XSTile> CreatePathFinderTileDict()
        {
            var ret = new Dictionary<Vector3Int, XSTile>();
            var gridHelper = Component.FindObjectOfType<XSGridHelper>();
            if (gridHelper == null)
                return ret;

            var tileDataList = gridHelper.GetTileDataArray();
            if (tileDataList == null || tileDataList.Length == 0)
                return ret;

            // 默认sprite.size为1
            var tileData = tileDataList.First();

            this.TileSize = Mathf.FloorToInt(tileData.gameObject.transform.localScale.x);
            var sprite = tileData.GetComponent<SpriteRenderer>();
            if (sprite)
                this.TileSize *= Mathf.FloorToInt(sprite.size.x);

            // 遍历Tile
            tileDataList.ToList().ForEach(tileData => AddXSTile(tileData, ret));

            return ret;
        }

        /// <summary>
        /// 添加XSTile到字典中
        /// </summary>
        /// <param name="tileData"></param>
        /// <param name="tileDict"></param>
        /// <returns></returns>
        public bool AddXSTile(XSTileData tileData, Dictionary<Vector3Int, XSTile> tileDict)
        {
            var tilePos = this.WorldToTile(tileData.transform.position);
            if (tileDict.ContainsKey(tilePos))
            {
                Debug.LogError("GridMgr.CreatePathFinderTileDict: 已经存在相同的tilePos：" + tilePos);
                GameObject.DestroyImmediate(tileData.gameObject);
                return false;
            }
            else
            {
                var tile = new XSTile(tilePos, tileData.transform.position, tileData.Cost, tileData);
                tileDict.Add(tilePos, tile);
                return true;
            }
        }
    }
}