/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:38:22
/// @Description: show tile manager
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public class XSGridShowMgr
    {
        /************************* variable begin ***********************/
        protected XSIGridShowRegion MoveShowRegion { get; }

        /************************* variable  end  ***********************/

        public XSGridShowMgr(XSIGridShowRegion moveShowRegion)
        {
            this.MoveShowRegion = moveShowRegion;
        }

        /// <summary>
        /// show unit move range
        /// </summary>
        /// <param name="unit"> which unit </param>
        public virtual List<Vector3> ShowMoveRegion(XSIUnitNode unit)
        {
            if (this.MoveShowRegion == null ||this.MoveShowRegion.IsNull())
            {
                return new List<Vector3>();
            }

            var moveRegion = unit.GetMoveRegion();
            this.MoveShowRegion.ShowRegion(moveRegion);
            return moveRegion;
        }

        /// <summary> clear unit move range show </summary>
        public virtual void ClearMoveRegion() => this.MoveShowRegion?.ClearRegion();
    }
}