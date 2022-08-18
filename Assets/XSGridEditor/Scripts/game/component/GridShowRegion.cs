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
    public class GridShowRegion : MonoBehaviour
    {
        /************************* 变量 begin ***********************/
        /// <summary> 高亮图块使用的prefab </summary>
        public GameObject Prefab { get; set; }

        /// <summary> sprite 排序规则 </summary>
        protected int SortOrder { get; set; }

        /************************* 变量  end  ***********************/

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="sortOrder"></param>
        public void Init(GameObject prefab, int sortOrder)
        {
            (this.Prefab, this.SortOrder) = (prefab, sortOrder);
        }

        /// <summary>
        /// 显示高亮范围
        /// </summary>
        /// <param name="worldPosList">图块所在的网格坐标</param>
        public void ShowRegion(List<Vector3> worldPosList)
        {
            if (this.Prefab == null)
                return;

            worldPosList.ForEach(pos =>
            {
                var obj = GameObject.Instantiate(this.Prefab, this.transform);
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
        public void ClearRegion() => XSUG.RemoveChildren(this.transform.gameObject);
    }
}