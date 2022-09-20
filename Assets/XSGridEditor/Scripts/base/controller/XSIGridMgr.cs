/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: interface to XSGridMgr
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;

namespace XSSLG
{
    /// <summary>interface to XSGridMgr </summary>
    public interface XSIGridMgr
    {

        List<XSTile> GetAllTiles();

        void ClearAllTiles();

        void Init(XSGridHelper helper);

        /// <summary>
        /// change world position to tile position
        /// </summary>
        /// <param name="worldPos">unity`s world position</param>
        Vector3Int WorldToTile(Vector3 worldPos);

        /// <summary>
        /// get the center position of the tile from a world position
        /// </summary>
        /// <param name="worldPos">unity`s world position</param>
        Vector3 WorldToTileCenterWorld(Vector3 worldPos);

        Vector3 TileToTileCenterWorld(Vector3Int tilePos);

        XSTile AddXSTile(XSITileNode tileData);

        bool RemoveXSTile(Vector3 worldPos);
        
        /// <summary>
        /// get tile from a world position
        /// </summary>
        /// <param name="worldPos">unity`s world position</param>
        bool GetXSTile(Vector3 worldPos);

       bool GetXSTile(Vector3 worldPos, out XSTile tile);
        
        bool GetXSTile(Vector3 worldPos, out XSTile tile, out Vector3Int tilePos);

        void UpdateTileSize(Vector3 tileSize);
        
        public Dictionary<Vector3, List<Vector3>> FindAllPath(XSTile srcTile, int moveRange);
    }
}