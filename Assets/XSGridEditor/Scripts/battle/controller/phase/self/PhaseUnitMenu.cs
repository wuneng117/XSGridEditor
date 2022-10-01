using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态单位打开菜单中
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 单位打开菜单中
    /// </summary>
    public class PhaseUnitMenu : PhaseBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            //PHASETODO UI显示
            Debug.Assert(logic.ActionUnit != null);
            this.MenuActive(true, logic);

            // XSUG.CameraGoto(logic.ActionUnit.GetPosition());
            XSUG.CameraCanFreeMove(false);

            //ˢ�½�ɫ����
            // m_MainScene->setRoleInfoVisible(true, m_MainScene->getActionPlayer());
        }
        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            this.MenuActive(false, logic);
            XSUG.CameraCanFreeMove(true);
        }

        /// <summary> 开启关闭菜单，并相应的操作 </summary>
        private void MenuActive<T>(bool val, T logic) where T : BattleLogic
        {
            BattleNode battleNode = XSUG.GetBattleNode();
            if (val) 
            {
                var screenPos = XSU.WorldPosToScreenPos(logic.ActionUnit.GetPosition());
                battleNode.OpenRoleMenu(screenPos);
            }
            else
                battleNode.CloseRoleMenu();
            // battleNode.mouseCuror.Active = !val;
            // battleNode.camC.Active = !val;
        }

        public override void OnMouseUpRight<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseUpRight(logic, mouseTile);
            // var ui = XSInstance.UIMgr.CloseUI();
            // if (ui == null || ui == XSUG.GetBattleNode().unitMenu.gameObject)
            //     logic.Change(new )
        }
    }
}