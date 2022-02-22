using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @Author: xiaoshi
/// @Date:2021/5/26
/// @Description: 配合GridMgr使用，常用显示范围
/// </summary>
namespace XSSLG
{
    /// <summary> 常用显示范围 </summary>
    public class GridShowRegion
    {
        /************************* 变量 begin ***********************/
        /// <summary> 高亮图块的根节点路径 </summary>
        public string RootPath { get; }
        /// <summary> 高亮图块使用的精灵 </summary>
        public Sprite Asset { get; }
        /// <summary> 高亮图块使用的prefab </summary>
        public GameObject Prefab { get; }
        /// <summary> 网格坐标转世界坐标 </summary>
        public Func<Vector3Int, Vector3> TileToWorldHander { get; }

        /************************* 变量  end  ***********************/

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootPath">高亮图块的根节点路径</param>
        /// <param name="Asset">图片精灵</param>
        public GridShowRegion(string rootPath, Sprite Asset, GameObject prefab, Func<Vector3Int, Vector3> TileToWorldHander)
        {
            this.RootPath = rootPath;
            this.Asset = Asset;
            this.Prefab = prefab;
            this.TileToWorldHander = TileToWorldHander;
        }

        /// <summary>
        /// 显示高亮范围
        /// </summary>
        /// <param name="cellPosList">图块所在的网格坐标</param>
        public void ShowRegion(List<Vector3Int> cellPosList)
        {
            cellPosList.ForEach(pos =>
            {
                var obj = GameObject.Instantiate(this.Prefab);
                var parent = GameObject.Find(this.RootPath).transform;
                var worldPos = this.TileToWorldHander(pos);
                worldPos += new Vector3(0, 0.12f, 0);   // 地面有抬高一点的
                var obj = new GameObject();
                obj.transform.parent = parent;
                obj.transform.position = worldPos;
                var spr = obj.AddComponent<SpriteRenderer>();
                spr.sprite = Resources.Load(this.AssetPath, typeof(Sprite)) as Sprite;
                spr.sortingLayerName = "Region";
            });
        }

        /// <summary> 清除高亮显示 </summary>
        public void ClearRegion() => XSU.RemoveChildren(GameObject.Find(this.RootPath));
    }
}