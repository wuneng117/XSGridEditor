/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile 管理类，负责tile 坐标转化，数据等功能
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace XSSLG
{
    /// <summary> tile 管理类，负责tile 坐标转化，数据等功能 </summary>
    public class GridMgr : GridMgrBase
    {
        /// <summary> tile父节点，提供坐标系用于tilepos和worldpos的转换，如此一来我们就可以移动这个节点来调整tile整体的位置</summary>
        private Transform TileRoot { get; }
        /// <summary> tile 大小，用来计算 tilePos </summary>
        private Vector3 TileSize { set; get; } = Vector3.zero;

        public GridMgr(XSGridHelper helper)
        {
            this.TileRoot = helper?.TileRoot;
            var grid = this.TileRoot?.GetComponent<Grid>();
            if (grid)
                // y和z换一下是因为Grid组件里y表示横坐标，z表示高度，而我们的tile为了和3d空间一直，里y表示高度，z表示横坐标
                this.TileSize = new Vector3(grid.cellSize.x, grid.cellSize.z, grid.cellSize.y);
            else
                this.TileSize = new Vector3(1, 1, 1);
        }

        public override Vector3 TileToWorld(Vector3Int tilePos)
        {
            var ret = new Vector3(0, 0, 0);

            // if (tilePos.x < 0 || tilePos.z < 0)
            //     return ret;

            tilePos.y = 0;
            var tile = this.GetTile(tilePos);
            if (tile == null) return ret;

            return tile.WorldPos;
        }

        public override Vector3Int WorldToTile(Vector3 worldPos)
        {
            var ret = new Vector3Int(-1, 0, -1);
            if (this.TileRoot == null)
                return ret;

            var localPos = this.TileRoot.InverseTransformPoint(worldPos);
            ret.x = Mathf.FloorToInt((localPos.x) / this.TileSize.x);
            ret.z = Mathf.FloorToInt((localPos.z) / this.TileSize.z);
            return ret;
        }
        public override Vector3 WorldToTileCenterWorld(Vector3 worldPos)
        {
            var ret = Vector3.zero;
            if (this.TileRoot == null)
                return ret;

            var tilePos = this.WorldToTile(worldPos);
            return this.TileToTileCenterWorld(tilePos);
        }

        protected override Vector3 TileToTileCenterWorld(Vector3Int tilePos)
        {
            var ret = Vector3.zero;
            ret.x = tilePos.x * this.TileSize.x + (float)this.TileSize.x / 2;
            ret.z = tilePos.z * this.TileSize.z + (float)this.TileSize.z / 2;
            ret = this.TileRoot.TransformPoint(ret);
            ret.y = 0;
            return ret;
        }

        protected override Dictionary<Vector3Int, XSTile> CreatePathFinderTileDict(XSGridHelper helper)
        {
            var ret = new Dictionary<Vector3Int, XSTile>();
            if (helper == null)
                return ret;

            var tileDataList = helper.GetTileDataArray();
            if (tileDataList == null || tileDataList.Length == 0)
                return ret;

            // 默认sprite.size为1
            var tileData = tileDataList.First();

            // TODO tile要适配大小刚好为Tile
            // this.TileSize = Mathf.FloorToInt(tileData.gameObject.transform.localScale.x);
            // var sprite = tileData.GetComponent<SpriteRenderer>();
            // if (sprite)
            //     this.TileSize *= Mathf.FloorToInt(sprite.size.x);

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
            // 判断 tileDict[tilePos].Node 是因为实际节点可能是被其他情况下清除了
            if (tileDict.ContainsKey(tilePos) && tileDict[tilePos].Node != null)
            {
                Debug.LogError("GridMgr.AddXSTile: 已经存在相同的tilePos：" + tilePos);
                return false;
            }
            else
            {
                var tile = new XSTile(tilePos, tileData.transform.position, tileData.Cost, tileData);
                tileDict.Add(tilePos, tile);
                return true;
            }
        }

        /// <summary>
        /// 从字典中删除XSTile
        /// </summary>
        /// <param name="tileData"></param>
        /// <param name="tileDict"></param>
        /// <returns></returns>
        public bool RemoveXSTile(XSTileData tileData, Dictionary<Vector3Int, XSTile> tileDict)
        {
            var tilePos = this.WorldToTile(tileData.transform.position);
            var ret = false;

            if (tileDict.ContainsKey(tilePos))
            {
                tileDict.Remove(tilePos);
                ret = true;
            }
            else
                Debug.LogError("GridMgr.RemoveXSTile: 这个位置上不存在tile，tilePos：" + tilePos);


            UnityUtils.RemoveObj(tileData.gameObject);
            return ret;
        }

        /// <summary>
        /// 添加XSTile
        /// </summary>
        /// <param name="tileData"></param>
        /// <returns></returns>
        public void UpdateTileSize(Vector3 tileSize)
        {
            this.TileSize = new Vector3(tileSize.x, tileSize.z, tileSize.y);
            foreach (var tile in this.TileDict.Values)
            {
                if (tile.Node != null)
                {
                    var newWorldPos = this.TileToTileCenterWorld(tile.TilePos);
                    tile.Node.transform.position = newWorldPos;
                    tile.WorldPos = newWorldPos;
                    var tileDataEditMode = tile.Node.GetComponent<XSTileDataEditMode>();
                    if (tileDataEditMode)
                        tileDataEditMode.PrevPos = tile.Node.transform.localPosition;
                }
            }
        }
    }
}