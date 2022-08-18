using System;
using System.Collections.Generic;
// using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// @Author: xiaoshi
/// @Date:2021/5/26
/// @Description: 配合GridMgr使用，常用显示范围
/// </summary>
这个应该写成component，因为这个只和unity内的显示相关
namespace XSSLG
{
    /// <summary> 常用显示范围 </summary>
    public class GridShowRegion
    {
        /************************* 变量 begin ***********************/
        /// <summary> 高亮图块的根节点路径 </summary>
        public string RootPath { get; }
        /// <summary> 高亮图块使用的prefab </summary>
        public GameObject Prefab { get; }

        /// <summary> sprite 排序规则 </summary>
        protected int SortOrder { get; }

        protected Transform Root { get; set; }

        /************************* 变量  end  ***********************/

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootPath">高亮图块的根节点路径</param>
        public GridShowRegion(string rootPath, GameObject prefab, int sortOrder)
        {
            (this.RootPath,  this.Prefab, this.SortOrder) = (rootPath, prefab, sortOrder);
            this.CreateRootNode();
        }

        protected virtual void CreateRootNode()
        {
            var parent = XSInstance.Instance.GridHelper?.transform;
            if (parent == null) 
                return;

            this.Root = new GameObject(this.RootPath).transform;
            this.Root.SetParent(parent);
        }

        /// <summary>
        /// 显示高亮范围
        /// </summary>
        /// <param name="worldPosList">图块所在的网格坐标</param>
        public void ShowRegion(List<Vector3> worldPosList)
        {
            if (this.Root == null ||  this.Prefab == null)
                return;

            worldPosList.ForEach(pos =>
            {
                var obj = GameObject.Instantiate(this.Prefab, this.Root);
                if (obj == null) 
                    return;

                // 设置layer默认。不要遮挡射线到 tile 的检测
                obj.layer = LayerMask.NameToLayer("Default");
                obj.transform.position = pos;

                var spr = obj.GetComponentInChildren<SpriteRenderer>();
                spr.sortingOrder = this.SortOrder;
            });
        }

        /// <summary> 清除高亮显示 </summary>
        public void ClearRegion() 
        {
            if (this.Root == null)
                return;

            XSUG.RemoveChildren(this.Root.gameObject);
        } 
    }
}