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
    using TileDict = Dictionary<Vector3Int, XSTile>;

    /// <summary> tile 管理类，负责tile 坐标转化，数据等功能 </summary>
    public class GridMgr : IGridMgr
    {
        /// <summary> 四边形地图链接格子就是这4个位置偏移 </summary>
        private static readonly Vector3Int[] NearPosArray = { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, -1), new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 1), };

        /// <summary> 以tilepos为key存储所有tile。 </summary>
        public TileDict TileDict { get; protected set; } = new TileDict();

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

        public Vector3 TileToWorld(Vector3Int tilePos)
        {
            var ret = new Vector3(0, 0, 0);

            // if (tilePos.x < 0 || tilePos.z < 0)
            //     return ret;

            tilePos.y = 0;
            var tile = this.GetTile(tilePos);
            if (tile == null) return ret;

            return tile.WorldPos;
        }

        public Vector3Int WorldToTile(Vector3 worldPos)
        {
            var ret = new Vector3Int(-1, 0, -1);
            if (this.TileRoot == null)
                return ret;

            var localPos = this.TileRoot.InverseTransformPoint(worldPos);
            ret.x = Mathf.FloorToInt((localPos.x) / this.TileSize.x);
            ret.z = Mathf.FloorToInt((localPos.z) / this.TileSize.z);
            return ret;
        }
        public Vector3 WorldToTileCenterWorld(Vector3 worldPos)
        {
            var ret = Vector3.zero;
            if (this.TileRoot == null)
                return ret;

            var tilePos = this.WorldToTile(worldPos);
            return this.TileToTileCenterWorld(tilePos);
        }

        protected Vector3 TileToTileCenterWorld(Vector3Int tilePos)
        {
            var ret = Vector3.zero;
            ret.x = tilePos.x * this.TileSize.x + (float)this.TileSize.x / 2;
            ret.z = tilePos.z * this.TileSize.z + (float)this.TileSize.z / 2;
            ret = this.TileRoot.TransformPoint(ret);
            ret.y = 0;
            return ret;
        }

        protected void CreateXSTileDict(XSGridHelper helper)
        {
            this.TileDict = new TileDict();
            if (helper == null)
                return;

            var tileDataList = helper.GetTileDataList();
            if (tileDataList == null || tileDataList.Count == 0)
                return;

            // TODO tile要适配大小刚好为Tile
            // this.TileSize = Mathf.FloorToInt(tileData.gameObject.transform.localScale.x);
            // var sprite = tileData.GetComponent<SpriteRenderer>();
            // if (sprite)
            //     this.TileSize *= Mathf.FloorToInt(sprite.size.x);

            // 遍历Tile
            tileDataList.ForEach(tileData => this.AddXSTile(tileData));
        }

        /// <summary>
        /// 添加XSTile到字典中
        /// </summary>
        /// <param name="tileData"></param>
        /// <returns></returns>
        public bool AddXSTile(XSTileData tileData)
        {
            var tilePos = this.WorldToTile(tileData.transform.position);
            // 判断 tileDict[tilePos].Node 是因为实际节点可能是被其他情况下清除了
            if (this.TileDict.ContainsKey(tilePos) && this.TileDict[tilePos].Node != null)
            {
                Debug.LogError("GridMgr.AddXSTile: 已经存在相同的tilePos：" + tilePos);
                return false;
            }
            else
            {
                if (!UnityUtils.IsEditor())
                {
                    var collider = tileData.gameObject.AddComponent<BoxCollider>();
                    collider.size = this.TileSize;
                }

                var tile = new XSTile(tilePos, tileData.transform.position, tileData.Cost, tileData);
                this.TileDict.Add(tilePos, tile);
                return true;
            }
        }

        /// <summary>
        /// 从字典中删除XSTile
        /// </summary>
        /// <param name="tileData"></param>
        /// <returns></returns>
        public bool RemoveXSTile(XSTileData tileData)
        {
            var tilePos = this.WorldToTile(tileData.transform.position);
            var ret = false;

            if (this.TileDict.ContainsKey(tilePos))
            {
                this.TileDict.Remove(tilePos);
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

                public virtual void Init(XSGridHelper helper)
        {
            this.CreateXSTileDict(helper);
            // 为每个PathFinderTile计算它的链接格子
            foreach (var pair in this.TileDict)
            {
                foreach (var pos in NearPosArray)
                {
                    var nearPos = pair.Key + pos;
                    if (this.TileDict.ContainsKey(nearPos))
                        pair.Value.NearTileList.Add(this.TileDict[nearPos]);
                }
            }
        }

        /// <summary> 以tilepos为key更新TileDict。 </summary>
        public void UpdateTileDict(XSTile tile)
        {
            // 更新tile必须是同一个Node
            if (this.TileDict.ContainsKey(tile.TilePos) && this.TileDict[tile.TilePos].Node == tile.Node)
                this.TileDict[tile.TilePos] = tile;
            else
                this.TileDict.Add(tile.TilePos, tile);
        }

        //1game
        public XSTile GetTile(Vector3 worldPos)
        {
            var tilelPos = this.WorldToTile(worldPos);
            return this.GetTile(tilelPos);
        }

        public XSTile GetTile(Vector3Int tilePos)
        {
            if (this.TileDict.ContainsKey(tilePos))
                return this.TileDict[tilePos];
            else
                return null;
        }

        /// <summary>
        /// 返回所有的路径
        /// </summary>
        /// <param name="srcTile">起点tile</param>
        /// <param name="moveRange">移动范围，默认-1和小于0都表示不限制移动范围</param>
        /// <returns></returns>
        public Dictionary<Vector3, List<Vector3>> FindAllPath(XSTile srcTile, int moveRange) => PathFinder.FindAllPath(this.TileDict, srcTile, moveRange);
    }
}