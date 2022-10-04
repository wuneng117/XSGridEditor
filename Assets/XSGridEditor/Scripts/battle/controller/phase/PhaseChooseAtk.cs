using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态选择攻击的对象
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 选择攻击的对象
    /// </summary>
    public class PhaseChooseAtk : PhaseBase
    {
        /// <summary> 选中的技能 </summary>
        private SkillBase Skill { get; }
        /// <summary> 保存的网格地址，防止重复计算 </summary>
        private XSTile Tile { get; set; }
        /// <summary> 技能的攻击范围 </summary>
        private List<Vector3Int> AttackRegion { get; set; }

        public PhaseChooseAtk(SkillBase skill)
        {
            this.Skill = skill;
        }

        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            // 显示攻击范围
            this.AttackRegion = XSUG.GetBattleNode().GridShowMgr.ShowAttackRegion(logic.UnitMgr.ActionUnit, this.Skill);
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            var gridShowMgr = XSUG.GetBattleNode().GridShowMgr;
            // TODO 
            // gridShowMgr.ClearAttackEffectRegion();
            // gridShowMgr.ClearAttackRegion();
        }

        public override void OnMouseUpLeft<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseUpLeft(logic, mouseTile);
            if (mouseTile == null)
            {
                return;
            }

            // 要在攻击范围内的格子
            if (!this.AttackRegion.Contains(mouseTile.TilePos))
                return;

            var onTriggerData = new OnTriggerDataCommon(logic.UnitMgr.ActionUnit, mouseTile);
            if (!this.Skill.Trigger.CanRelease(onTriggerData))
                return;

            logic.Change(new PhaseUnitAtk(this.Skill, mouseTile));
        }

        public override void OnMouseUpRight<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseUpRight(logic, mouseTile);
            logic.Change(new PhaseUnitMenu());
        }

        public override void OnMouseMove<T>(T logic, XSTile mouseTile)
        {
            base.OnMouseMove(logic, mouseTile);
            if (mouseTile == null)
            {
                return;
            }

            XSUG.GetBattleNode().UpdateUnitInfoTip(mouseTile);
        
            if (mouseTile != this.Tile)
            {
                this.Tile = mouseTile;
                var battleNode = XSUG.GetBattleNode();
                battleNode.GridShowMgr.ClearAttackEffectRegion();
                // 要在攻击范围内的格子
                if (this.AttackRegion.Contains(mouseTile.TilePos))
                {
                    battleNode.GridShowMgr.ShowAttackEffectRegion(logic.UnitMgr.ActionUnit, this.Skill, mouseTile);
                }
            }
        }
    }
}