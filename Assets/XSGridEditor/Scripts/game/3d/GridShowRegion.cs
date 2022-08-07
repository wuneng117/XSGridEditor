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

        /// <summary> sprite 排序规则 </summary>
        private int SortOrder { get; }

        /************************* 变量  end  ***********************/

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootPath">高亮图块的根节点路径</param>
        /// <param name="Asset">图片精灵</param>
        public GridShowRegion(string rootPath, Sprite Asset, GameObject prefab, int sortOrder)
        {
            this.RootPath = rootPath;
            this.Asset = Asset;
            this.Prefab = prefab;
            this.SortOrder = sortOrder;
            this.CreateRootNode();
        }

        protected virtual void CreateRootNode()
        {
            if (GameObject.Find(this.RootPath))
             return;

            var root = XSInstance.Instance.GridHelper?.transform;
            if (root ==null) return;

            // 取名字，路径不要
            var parentObj = new GameObject(this.RootPath);
            parentObj.transform.SetParent(root.transform);
        }

        /// <summary>
        /// 显示高亮范围
        /// </summary>
        /// <param name="worldPosList">图块所在的网格坐标</param>
        public void ShowRegion(List<Vector3> worldPosList)
        {
            worldPosList.ForEach(pos =>
            {
                var parent = GameObject.Find(this.RootPath)?.transform;
                if (parent ==null) return;

                var obj = GameObject.Instantiate(this.Prefab, parent);
                if (obj == null) return;

                // 设置layer默认。不要遮挡射线到 tile 的检测
                obj.layer = LayerMask.NameToLayer("Default");
                obj.transform.position = pos;

                var spr = obj.GetComponentInChildren<SpriteRenderer>();
                spr.sprite = this.Asset;
                spr.sortingOrder = this.SortOrder;
            });
        }

        /// <summary> 清除高亮显示 </summary>
        public void ClearRegion() 
        {
            var parentObj = GameObject.Find(this.RootPath);
            if (parentObj ==null) return;

            XSUG.RemoveChildren(parentObj);
        } 
    }
}