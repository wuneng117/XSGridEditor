using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态选择移动的位置
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 选择移动的位置
    /// </summary>
    public class PhaseChooseMove : PhaseChooseMoveBase
    {
        public override void OnMouseUpLeft<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseUpLeft(logic, mouseTile);

            if (mouseTile == null)
            {
                return;
            }

            // 要在移动范围内的格子
            if (!this.MoveRegion.Contains(mouseTile.WorldPos))
                return;

            if (logic.UnitMgr.ActionUnit.WalkTo(mouseTile))
                logic.Change(new PhaseUnitMove());
        }

        public override void OnMouseUpRight<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseUpRight(logic, mouseTile);
            logic.Change(new PhaseChooseUnit());
        }

        public override void OnMouseMove<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseMove(logic, mouseTile);
            if (mouseTile == null)
            {
                return;
            }
            
            XSU.GetBattleNode().UpdateUnitInfoTip(mouseTile);
        }
    }
}