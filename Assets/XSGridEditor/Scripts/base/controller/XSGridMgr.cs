/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile 管理类，负责tile 坐标转化，数据等功能
/// </summary>
using System.Collections.Generic;
using System.Linq;

using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;
using Mathf = UnityEngine.Mathf;
using Debug = UnityEngine.Debug;

namespace XSSLG
{
    using TileDict = Dictionary<Vector3Int, XSTile>;

    /// <summary> tile 管理类，负责tile 坐标转化，数据等功能 </summary>
    public class XSGridMgr : XSIGridMgr
    {
        /// <summary> 四边形地图链接格子就是这4个位置偏移 </summary>
        protected static readonly Vector3Int[] NearPosArray = { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, -1), new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 1), };

        /// <summary> 以tilepos为key存储所有tile。 </summary>
        protected TileDict TileDict { get; set; } = new TileDict();

        public virtual List<XSTile> GetAllTiles() => this.TileDict.Values.ToList();

        public virtual void ClearAllTiles()
        {
           this.TileDict.Clear();
           this.TileRoot.ClearAllTiles();
        }

        /// <summary> 提供坐标系用于tilepos和worldpos的转换，如此一来我们就可以移动这个节点来调整tile整体的位置</summary>
        protected XSITileRoot TileRoot { get; }

        /// <summary> tile 大小，用来计算 tilePos </summary>
        protected Vector3 TileSize { set; get; } = Vector3.zero;

        public XSGridMgr(XSITileRoot tileRoot, Vector3 cellSize)
        {
            this.TileRoot = tileRoot;

            // y和z换一下是因为Grid组件里y表示横坐标，z表示高度，而我们的tile为了和3d空间一直，里y表示高度，z表示横坐标
            this.TileSize = new Vector3(cellSize.x, cellSize.z, cellSize.y);
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
                    if (this.GetXSTile(nearPos, out var tile) && 
                        tile.PassNearRule(pair.Value, helper.TileOffYMax) &&
                        pair.Value.PassNearRule(tile, helper.TileOffYMax))
                    {
                        pair.Value.NearTileList.Add(tile);
                    }
                }
            }
        }

        public virtual Vector3Int WorldToTile(Vector3 worldPos)
        {
            var ret = Vector3Int.zero;
            if (this.TileRoot == null || this.TileRoot.IsNull())
            {
                return ret;
            }

            var localPos = this.TileRoot.InverseTransformPoint(worldPos);
            ret.x = Mathf.FloorToInt((localPos.x) / this.TileSize.x);
            ret.z = Mathf.FloorToInt((localPos.z) / this.TileSize.z);
            return ret;
        }

        public virtual Vector3 WorldToTileCenterWorld(Vector3 worldPos)
        {
            var tilePos = this.WorldToTile(worldPos);
            return this.TileToTileCenterWorld(tilePos);
        }

        public virtual Vector3 TileToTileCenterWorld(Vector3Int tilePos)
        {
            var ret = Vector3.zero;
            ret.x = tilePos.x * this.TileSize.x + (float)this.TileSize.x / 2;
            ret.z = tilePos.z * this.TileSize.z + (float)this.TileSize.z / 2;
            ret = this.TileRoot.TransformPoint(ret);
            ret.y = 0;
            return ret;
        }

        protected virtual void CreateXSTileDict(XSGridHelper helper)
        {
            this.TileDict = new TileDict();
            if (helper == null)
            {
                return;
            }

            var tileDataList = helper.GetTileNodeList();
            if (tileDataList == null || tileDataList.Count == 0)
            {
                return;
            }

            // 遍历Tile
            tileDataList.ForEach(tileData => this.AddXSTile(tileData));
        }

        /// <summary>
        /// 添加XSTile到字典中
        /// </summary>
        /// <param name="tileNode"></param>
        /// <returns></returns>
        public virtual XSTile AddXSTile(XSITileNode tileNode)
        {
            var tilePos = this.WorldToTile(tileNode.WorldPos);
            if (this.GetXSTile(tilePos))
            {
                Debug.LogError("GridMgr.AddXSTile: 已经存在相同的tilePos：" + tilePos);
                return null;
            }
            else
            {
                if (!XSUnityUtils.IsEditor())
                {
                    tileNode.AddBoxCollider(this.TileSize);
                }

                var tile = tileNode.CreateXSTile(tilePos);
                this.TileDict.Add(tilePos, tile);
                return tile;
            }
        }

        /// <summary>
        /// 从字典中删除XSTile
        /// </summary>
        /// <param name="worldPos">tila世界坐标</param>
        /// <returns></returns>
        public virtual bool RemoveXSTile(Vector3 worldPos)
        {
            if (this.GetXSTile(worldPos, out var tile, out var tilePos))
            {
                tile.Node.RemoveNode();
                this.TileDict.Remove(tilePos);
                return true;
            }
            else
            {
                Debug.Log("GridMgr.RemoveXSTile: 这个位置上不存在tile，tilePos：" + tilePos);
                return false;
            }
        }

        public virtual bool GetXSTile(Vector3 worldPos) => this.GetXSTile(worldPos, out var tile);

        public virtual bool GetXSTile(Vector3 worldPos, out XSTile tile) => this.GetXSTile(worldPos, out tile, out var tilsPos);
        
        public virtual bool GetXSTile(Vector3 worldPos, out XSTile tile, out Vector3Int tilePos)
        {
            tilePos = this.WorldToTile(worldPos);
            return this.GetXSTile(tilePos, out tile);
        }

        protected virtual bool GetXSTile(Vector3Int tilePos, out XSTile tile)
        {
            if (this.TileDict.TryGetValue(tilePos, out tile))
            {
                if (tile.Node == null || tile.Node.IsNull())
                {
                    this.TileDict.Remove(tilePos);
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

        public virtual void UpdateTileSize(Vector3 tileSize)
        {
            this.TileSize = new Vector3(tileSize.x, tileSize.z, tileSize.y);
            foreach (var tile in this.TileDict.Values)
            {
                if (tile.Node == null || tile.Node.IsNull())
                {
                    continue;
                }

                var newWorldPos = this.TileToTileCenterWorld(tile.TilePos);
                tile.Node.WorldPos = newWorldPos;
                tile.WorldPos = newWorldPos;
            }
        }


        /// <summary>
        /// 返回所有的路径
        /// </summary>
        /// <param name="srcTile">起点tile</param>
        /// <param name="moveRange">移动范围，默认-1和小于0都表示不限制移动范围</param>
        /// <returns></returns>
        public virtual Dictionary<Vector3, List<Vector3>> FindAllPath(XSTile srcTile, int moveRange) => XSPathFinder.FindAllPath(this.TileDict, srcTile, moveRange);
    }
}