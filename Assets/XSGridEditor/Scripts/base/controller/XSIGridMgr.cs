/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile 管理类接口，负责tile 坐标转化，数据等功能
/// </summary>
using System.Collections.Generic;
// using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;

namespace XSSLG
{
    /// <summary> tile 管理类接口，负责tile 坐标转化，数据等功能 </summary>
    public interface XSIGridMgr
    {

        List<XSTile> GetAllTiles();

        void ClearAllTiles();

        void Init(XSGridHelper helper);

        /// <summary>
        /// 从 worldPos 转为 tilePos
        /// </summary>
        /// <param name="worldPos">unity 的世界坐标</param>
        Vector3Int WorldToTile(Vector3 worldPos);

        /// <summary>
        /// 获取随意一点世界坐标对应 tile 的中心位置
        /// </summary>
        /// <param name="worldPos">unity 的世界坐标</param>
        Vector3 WorldToTileCenterWorld(Vector3 worldPos);

        Vector3 TileToTileCenterWorld(Vector3Int tilePos);

        XSTile AddXSTile(XSTileNode tileData);

        bool RemoveXSTile(Vector3 worldPos);
        
        /// <summary>
        /// 从 worldPos 获取tile
        /// </summary>
        /// <param name="worldPos">unity 的世界坐标</param>
        XSTile GetXSTile(Vector3 worldPos);


        void UpdateTileSize(Vector3 tileSize);
        
        public Dictionary<Vector3, List<Vector3>> FindAllPath(XSTile srcTile, int moveRange);
    }
}