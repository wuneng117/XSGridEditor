/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/5/4
/// @Description: tile 管理类接口，负责tile 坐标转化，数据等功能
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 管理类接口，负责tile 坐标转化，数据等功能 </summary>
    public interface IGridMgr
    {
        /// <summary> 寻路类，同时也存放 tile 的结构数据 PathFinderTile </summary>
        PathFinder PathFinder { get; }

        /// <summary>
        /// 从 tilePos 转为 worldPos
        /// </summary>
        /// <param name="tilePos">表示每个 tile 的坐标</param>
        Vector3 TileToWorld(Vector3Int tilePos);

        /// <summary>
        /// 从 worldPos 转为 tilePos
        /// </summary>
        /// <param name="worldPos">unity 的世界坐标</param>
        Vector3Int WorldToTile(Vector3 worldPos);

        /// <summary>
        /// 从 worldPos 获取tile
        /// </summary>
        /// <param name="worldPos">unity 的世界坐标</param>
        PathFinderTile GetTile(Vector3 worldPos);

        /// <summary>
        /// 从  tilePos 获取 tile
        /// </summary>
        /// <param name="tilePos">表示每个 tile 的坐标</param>
        PathFinderTile GetTile(Vector3Int tilePos);

        /// <summary>
        /// 获取随意一点世界坐标对应 tile 的中心位置
        /// </summary>
        /// <param name="worldPos">unity 的世界坐标</param>
        Vector3 WorldToTileCenterWorld(Vector3 worldPos);
    }
}