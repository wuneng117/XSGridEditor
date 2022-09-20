/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile management class, responsible for tile coordinate transformation, data and other functions
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

    /// <summary>tile management class, responsible for tile coordinate transformation, data and other functions </summary>
    public class XSGridMgr : XSIGridMgr
    {
        /// <summary> 4 near tile to each tile for squre map </summary>
        protected static readonly Vector3Int[] NearPosArray = { Vector3Int.left, Vector3Int.back, Vector3Int.right, Vector3Int.forward, };

        /// <summary> key is tilepos, value is tile </summary>
        protected TileDict TileDict { get; set; } = new TileDict();

        public virtual List<XSTile> GetAllTiles() => this.TileDict.Values.ToList();

        public virtual void ClearAllTiles()
        {
           this.TileDict.Clear();
           this.TileRoot.ClearAllTiles();
        }

        /// <summary> tile`s parent，we can change this gameobject position to move all tile`s position </summary>
        protected XSITileRoot TileRoot { get; }

        /// <summary> used to calculate tilePos </summary>
        protected Vector3 TileSize { set; get; } = Vector3.zero;

        public XSGridMgr(XSITileRoot tileRoot, Vector3 cellSize)
        {
            this.TileRoot = tileRoot;

            // reverse y and z, because the Grid component is different from the 3d space
            this.TileSize = new Vector3(cellSize.x, cellSize.z, cellSize.y);
        }

        public virtual void Init(XSGridHelper helper)
        {
            this.CreateXSTileDict(helper);
            // calculate near tile for each tile
            foreach (var pair in this.TileDict)
            {
                foreach (var pos in NearPosArray)
                {
                    var nearPos = pair.Key + pos;
                    if (this.GetXSTile(nearPos, out var tile) && 
                        tile.PassNearRule(pair.Value, pos, helper.TileOffYMax) &&
                        pair.Value.PassNearRule(tile, -pos, helper.TileOffYMax))
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

            // traverse tile
            tileDataList.ForEach(tileData => this.AddXSTile(tileData));
        }

        /// <summary>
        /// add tile to dict
        /// </summary>
        /// <param name="tileNode"></param>
        /// <returns></returns>
        public virtual XSTile AddXSTile(XSITileNode tileNode)
        {
            var tilePos = this.WorldToTile(tileNode.WorldPos);
            if (this.GetXSTile(tilePos))
            {
                this.RemoveXSTile(tileNode.WorldPos);
            }
            
            // setting the camera range
            if (!XSUnityUtils.IsEditor())
            {
                tileNode.AddBoxCollider(this.TileSize);
            }

            var tile = tileNode.CreateXSTile(tilePos);
            this.TileDict.Add(tilePos, tile);
            return tile;
        }

        /// <summary>
        /// remove tile from dict
        /// </summary>
        /// <param name="worldPos">tila's world position</param>
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
                Debug.Log("GridMgr.RemoveXSTile: the tile is not exist, tilePos：" + tilePos);
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
        /// get all paths
        /// </summary>
        /// <param name="srcTile">beginning tile</param>
        /// <param name="moveRange"> -1 or less than 0 means no limit to move range </param>
        /// <returns></returns>
        public virtual Dictionary<Vector3, List<Vector3>> FindAllPath(XSTile srcTile, int moveRange) => XSPathFinder.FindAllPath(srcTile, moveRange);
    }
}