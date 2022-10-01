using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 打开主菜单，比如要回合结束
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 单位打开菜单中
    /// </summary>
    public class PhaseMainMenu : PhaseBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            this.MenuActive(true);
            XSUG.CameraCanFreeMove(false);
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            this.MenuActive(false);
            XSUG.CameraCanFreeMove(true);
        }

        /// <summary> 开启关闭菜单，并相应的操作 </summary>
        private void MenuActive(bool val)
        {
            BattleNode battleNode = XSUG.GetBattleNode();
            if (val) 
                battleNode.OpenMainMenu();
            else
                battleNode.CloseMainMenu();
            // battleNode.mouseCuror.Active = !val;
            // battleNode.camC.Active = !val;
        }
    }
}