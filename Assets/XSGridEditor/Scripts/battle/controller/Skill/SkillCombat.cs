/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/10
/// @Description: 主动技能，攻击/战技
/// </summary>
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary> 主动技能，攻击/战技 </summary>
    public class SkillCombat : SkillBase
    {
        /************************* 变量 begin ***********************/
        /************************* 变量  end  ***********************/
        public SkillCombat(SkillData data, UnitBase unit) : base(data, unit)
        {

        }

        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool CanRelease(ReleaseData data)
        {
            if (data.Target.Count <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="data"></param>
        public override bool Release(ReleaseData data)
        {
            var ret = base.Release(data);
            if (!ret)
                return false;

            var srdDamageList = new List<int>();
            int dstDamage = 0;

            data.Target.ForEach(unit =>
            {
                // 躲避计算
                var damage = this.CaculateDamage(this.Unit, unit);
                var srdDamage = CombatCaculate.ApplyDamage(this.Unit, unit, damage);
                srdDamageList.Add(srdDamage);
            });
            // // 1v1时会有反击 TODO 动画有点麻烦
            // if (data.Target.Count == 1)
            //     dstDamage = this.ApplyDamage(data.Target[0], this.Unit);

            BattleEmitter.Instance.Emit(XSDefine.TriggerType.AfterAttack, new OnTriggerDataAttack(this.Unit, data.Target, srdDamageList, dstDamage));
            return true;
        }

        /// <summary>
        /// 计算伤害
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public int CaculateDamage(UnitBase src, UnitBase dest)
        {
            //攻击＝力量＋武器威力＋技能＋骑士团物攻,全部通过Stat加
            float damage = src.GetStat().Str.GetFinal() - dest.GetStat().Def.GetFinal();
            damage = damage * src.Table.PhyDamageFactor() *  dest.Table.PhyImmunityFactor();
            return (int)damage;
        }
    }
}