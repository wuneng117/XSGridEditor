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
    public class AIChooseAction : PhaseBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            Debug.Assert(logic.UnitMgr.ActionUnit != null);
            XSUG.CameraGoto(logic.UnitMgr.ActionUnit.WorldPos);
        }

        public override void Update<T>(T logic)
        {
            base.Update(logic);
            // 摄像机移动完了
            if (!XSUG.GetBattleNode().XSCamera.IsMoving)
            {
                // 能打到人就不要移动了
                if (this.CanAttack(logic))
                {
                }
                else if (!logic.UnitMgr.ActionUnit.IsMoved())
                {
                    logic.Change(new AIUnitMove());
                }
                else
                {
                    //TODO 道具之类的

                    logic.UnitMgr.ActionUnit.SetActived();
                    logic.Change(new AIChooseUnit());
                }
            }
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            var gridShowMgr = XSUG.GetBattleNode().GridShowMgr;
            gridShowMgr.ClearAttackEffectRegion();
            gridShowMgr.ClearAttackRegion();
        }

        private bool CanAttack<T>(T logic) where T : BattleLogic
        {
            var skill = logic.UnitMgr.ActionUnit.Table.SkillTable.AttackSkill;
            var AttackRegion = XSUG.GetBattleNode().GridShowMgr.ShowAttackRegion(logic.UnitMgr.ActionUnit, skill);
            foreach (var tilePos in AttackRegion)
            {
                var tile = XSU.GridMgr.GetXSTile(tilePos);
                if (tile == null)
                    continue;

                var onTriggerData = new OnTriggerDataCommon(logic.UnitMgr.ActionUnit, tile);
                if (!skill.Trigger.CanRelease(onTriggerData))
                    continue;

                // 内容基本是一样的，就是跳转的阶段不一样
                logic.Change(new AIUnitAtk(skill, tile));
                return true;
            }

            //TODO 战技也要差不多和上面一样做一遍

            XSUG.GetBattleNode().GridShowMgr.ClearAttackRegion();
            return false;
        }
    }
}