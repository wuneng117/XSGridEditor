using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态选择行动的单位
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 选择行动的单位
    /// </summary>
    public class PhaseChooseUnit : PhaseBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            // 要选择行动的unit，把之前的清除下
            XSUG.GetBattleLogic().UnitMgr.ClearActionUnit();
        }

        public override void OnMouseUpLeft<T>(T logic , XSTile mouseTile)
        {
            base.OnMouseUpLeft(logic, mouseTile);
            // 没有界面显示了才能点击 unit
            if (!XSUG.UIMgr.IsEmpty || mouseTile == null)
            {
                return;
            }

            // PhaseChooseUnit下点到了可以行动的单位，
            if (logic.UnitMgr.SetActionUnit(mouseTile, GroupType.Self))
            {
                logic.Change(new PhaseChooseMove());
            }
        }

        public override void OnMouseUpRight<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseUpRight(logic, mouseTile);
            // 没有界面显示了才能点击 unit
            if (!XSUG.UIMgr.IsEmpty)
            {
                return;
            }

            if (mouseTile != null)
            {
                BattleNode battleNode = XSUG.GetBattleNode();
                var unit = logic.UnitMgr.GetUnitByCellPosition(mouseTile.TilePos);
                if (unit != null)
                    battleNode.OpenRolePanel(unit);
                else
                    battleNode.OpenMainMenu();
            }
        }

        public override void OnMouseMove<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseMove(logic, mouseTile);
            // 没有界面显示了才能点击 unit
            if (!XSUG.UIMgr.IsEmpty || mouseTile == null)
            {
                return;
            }

            XSUG.GetBattleNode().UpdateUnitInfoTip(mouseTile);
        }
    }
}