using System;
using System.Collections.Generic;
using System.Linq;
// using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using GameObject = UnityEngine.GameObject;  // TODO1

namespace XSSLG
{
    /// <summary>  </summary>
    public class XSGridShowMgr
    {
        /************************* 变量 begin ***********************/
        /// <summary> 移动范围显示 </summary>
        private XSGridShowRegionCpt MoveShowRegion { get; }

        /************************* 变量  end  ***********************/

        /// <summary> 初始化网格的贴花管理 </summary>
        public XSGridShowMgr(GameObject moveTilePrefab)
        {
            if (moveTilePrefab)
                this.MoveShowRegion = this.CreateMoveShowRegion(XSGridDefine.SCENE_GRID_MOVE, moveTilePrefab, 10);
        }

        protected virtual XSGridShowRegionCpt CreateMoveShowRegion(string rootPath, GameObject moveTilePrefab, int sortOrder)
        {
            var parent = XSInstance.Instance.GridHelper?.transform;
            if (parent == null)
                return null;

            var node = new GameObject(rootPath).transform;
            node.SetParent(parent);
            var showRegion = node.gameObject.AddComponent<XSGridShowRegionCpt>();
            showRegion.Init(moveTilePrefab, sortOrder);
            return showRegion;
        }

        /// <summary>
        /// 显示单位移动范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        public List<Vector3> ShowMoveRegion(XSUnitNode unit)
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