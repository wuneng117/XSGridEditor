using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态单位攻击中
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 单位攻击中
    /// </summary>
    public abstract class PhaseUnitAtkBase : PhaseBase
    {

        /// <summary> 选中的技能 </summary>
        protected SkillBase Skill { get; }
        protected XSTile Tile { get; }
        protected OnTriggerDataAttack AttackData { get; set; }

        public PhaseUnitAtkBase(SkillBase skill, XSTile tile) : base()
        {
            this.Skill = skill;
            this.Tile = tile;
        }

        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            Debug.Assert(logic.UnitMgr.ActionUnit != null);
            XSU.CameraCanFreeMove(false);

            BattleEmitter.Instance.On(TriggerType.AfterAttack, this.OnAfterAttack);

            // 朝向攻击点
            logic.UnitMgr.ActionUnit.Node.RotateTo(this.Tile);

            var onTriggerData = new OnTriggerDataCommon(logic.UnitMgr.ActionUnit, this.Tile);
            this.Skill.Trigger.Release(onTriggerData);
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            XSU.CameraCanFreeMove(true);
            logic.UnitMgr.ActionUnit.SetAttacked();
            // 攻击后刷新下tips。有可能数值改变了
            XSU.GetBattleNode().unitInfoTip.Refresh();
        }

        public override void Update<T>(T logic)
        {
            base.Update(logic);
            if (!logic.UnitMgr.ActionUnit.Node.IsAttacking)
                logic.Change(this.GetNextPhase());
        }

        protected abstract PhaseBase GetNextPhase();

        private void OnAfterAttack(OnTriggerDataBase data)
        {
            this.AttackData = (OnTriggerDataAttack)data;
            // // 是否有反击
            // var isCountBack = this.AttackData.Dst.Count == 1;
            ((Unit)this.AttackData.Src).Node.AttackAnimation();
            for (int index = 0; index < this.AttackData.Dst.Count; index++)
            {
                var unit = this.AttackData.Dst[index];
                var damage = this.AttackData.SrcCauseDamage[index];
                ((Unit)unit).Node.ApplyDamageAnimation(damage);
            }
        }
    }
}