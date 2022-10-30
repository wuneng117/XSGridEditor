/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/10
/// @Description: 主动技能，攻击/战技
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

                var damage = this.CaculateDamage(unit);
                var srdDamage = CombatCaculate.ApplyDamage(this.Unit, unit, damage);
                srdDamageList.Add(srdDamage);
            });
            // // 1v1时会有反击 TODO 动画有点麻烦
            // if (data.Target.Count == 1)
            //     dstDamage = this.ApplyDamage(data.Target[0], this.Unit);

            BattleEmitter.Instance.Emit(TriggerType.AfterAttack, new OnTriggerDataAttack(this.Unit, data.Target, srdDamageList, dstDamage));
            return true;
        }

        protected virtual bool GetIsHit()
        {
            // 己方命中

            // 面板命中=武器命中+技巧+技能补正。

            // 面板闪避=攻速+技能修正。

            // 实际命中=面板命中+连携补正-敌方面板闪避。

            // 从成长属性来说，命中受到技巧，速度和力量的影响（系数为1，1，~0.2），闪避受到速度和力量的影响（系数为1，1，~0.2）。

            // 弓每远两格降低20点命中。

            var ret = Random.Range(0, 100);
            return ret  > 80;
        }

        /// <summary>
        /// 计算伤害
        /// </summary>
        /// <param name="dest"></param>
        public int CaculateDamage(UnitBase dest)
        {
            var stat = this.Unit.GetStat();
            //攻击＝力量＋武器威力＋技能＋骑士团物攻,全部通过Stat加
            float damage = stat.Str.GetFinal() + this.GetSkillAddDamage(stat) - dest.GetStat().Def.GetFinal();
            damage = damage * this.Unit.Table.PhyDamageFactor() *  dest.Table.PhyImmunityFactor();
            return (int)damage;
        }

        /// <summary> 技能增加的伤害, TODOskillmagic类里应该也差不多 </summary>
        protected virtual int GetSkillAddDamage(Stat stat)
        {
            // TODO 没调试过呢!
            var ret = this.Data.StatSingleList.Aggregate(this.Data.DamageBase, (total, effect) => total + Mathf.RoundToInt(effect.Percent * (float)(stat.GetType().GetProperty(effect.Type)?.GetValue(stat) ?? 0)));
            return ret;
        }
    }
}