/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:38:22
/// @Description: 其它 tile 显示管理类
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    /// <summary>  </summary>
    public class XSGridShowMgr
    {
        /************************* 变量 begin ***********************/
        /// <summary> 移动范围显示 </summary>
        protected XSIGridShowRegion MoveShowRegion { get; }

        /************************* 变量  end  ***********************/

        /// <summary> 初始化网格的贴花管理 </summary>
        public XSGridShowMgr(XSIGridShowRegion moveShowRegion)
        {
            this.MoveShowRegion = moveShowRegion;
        }

        /// <summary>
        /// 显示单位移动范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        public virtual List<Vector3> ShowMoveRegion(XSIUnitNode unit)
        {
            if (this.MoveShowRegion == null)
            {
                return new List<Vector3>();
            }

            var moveRegion = unit.GetMoveRegion();
            this.MoveShowRegion.ShowRegion(moveRegion);
            return moveRegion;
        }

        /// <summary> 清除单位移动范围 </summary>
        public virtual void ClearMoveRegion() => this.MoveShowRegion?.ClearRegion();
    }
}