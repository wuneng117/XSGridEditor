using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary>  </summary>
    public class GridShowMgr
    {
        /************************* 变量 begin ***********************/
        /// <summary> 移动范围显示 </summary>
        private GridShowRegion MoveShowRegion { get; }

        /************************* 变量  end  ***********************/

        /// <summary> 初始化网格的贴花管理 </summary>
        public GridShowMgr(GameObject moveTilePrefab)
        {
            if (moveTilePrefab)
                this.MoveShowRegion = new GridShowRegion(XSGridDefine.SCENE_GRID_MOVE, moveTilePrefab, 10);
        }

        /// <summary>
        /// 显示单位移动范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        public List<Vector3> ShowMoveRegion(XSUnitData unit)
        {
            if (this.MoveShowRegion == null)
                return new List<Vector3>();

            var moveRegion = unit.GetMoveRegion();
            this.MoveShowRegion.ShowRegion(moveRegion);
            return moveRegion;
        }

        /// <summary> 清除单位移动范围 </summary>
        public void ClearMoveRegion() => this.MoveShowRegion?.ClearRegion();
    }
}