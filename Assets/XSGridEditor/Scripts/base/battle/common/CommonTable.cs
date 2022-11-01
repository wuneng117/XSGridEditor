using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 带管理的
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 套娃接口
    /// </summary>
    /// <typeparam name="TUPDATEDATA"></typeparam>
    public interface ICommonTable<TUPDATEDATA>
    {
        void OnTurnStart(TUPDATEDATA data);
        float PhyDamageFactor();
        float PhyImmunityFactor();
        void StartWork();
        void StopWork();
    }

    /// <summary>
    /// 带常用功能的skillbase和buffbase基类
    /// </summary>
    /// <typeparam name="TDATA"></typeparam>
    /// <typeparam name="TUPDATEDATA"></typeparam>
    public abstract class CommonTableItem<TDATA, TUPDATEDATA> : WorkItem<TDATA, TUPDATEDATA>, ICommonTable<TUPDATEDATA>
    {
        /// <summary> 定时几回合后使用 </summary>
        private CustomScheduler Scheduler { get; } = new CustomScheduler();
        protected CustomSchedulerItem Schedule(Action func, int interval, int repeat = -1, bool immediate = false, int delay = 0) => this.Scheduler.Schedule(func, interval, repeat, immediate, delay);
        protected CustomSchedulerItem ScheduleOnce(Action func, int delay) => this.Scheduler.ScheduleOnce(func, delay);
        protected void UnSchedule(CustomSchedulerItem scheduler) => this.Scheduler.UnSchedule(scheduler);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CommonTableItem(TDATA data) : base(data)
        {
        }

        public override void OnTurnStart(TUPDATEDATA data)
        {
            this.Scheduler.Update(1);
        }

        public abstract float GetSkillEffectProp(SkillEffectType type);
        public abstract bool GetSkillEffectFlag(SkillEffectType type);

        /// <summary> 减免伤害百分比必须大于0 </summary>
        public virtual float PhyImmunityFactor() => Math.Max(0, (1 - this.GetSkillEffectProp(SkillEffectType.PhyImmunityFactor)));
        public virtual float PhyDamageFactor() => (1 + this.GetSkillEffectProp(SkillEffectType.PhyDamageFactor));
        // public virtual float LawImmunityFactor() => (1 + this.GetSkillEffectProp(SkillEffectType.LawImmunityFactor));
        // public virtual float LawDamageFactor() => (1 + this.GetSkillEffectProp(SkillEffectType.LawDamageFactor));
        // public virtual float HealFactor() => (1 + this.GetSkillEffectProp(SkillEffectType.HealFactor));
        // public virtual float CreatureDurationTimeFactor() => (1 + this.GetSkillEffectProp(SkillEffectType.CreatureDurationTimeFactor)); // 召唤物持续时间修正
        // public virtual float DebuffDurationFactor() => (1 + this.GetSkillEffectProp(SkillEffectType.DebuffDurationFactor)); // debuff持续时间修正
        // public virtual bool CanMove() => !this.GetSkillEffectFlag(SkillEffectType.NoMove);
        // public virtual bool CanDie() => !this.GetSkillEffectFlag(SkillEffectType.NoDie);
        // public virtual bool CanGetDamage() => !this.GetSkillEffectFlag(SkillEffectType.NoGetDamage); // 是否能受到伤害
    }

    /// <summary>
    /// skilltable和bufftable的基类
    /// </summary>
    /// <typeparam name="TITEM"></typeparam>
    /// <typeparam name="TDATA"></typeparam>
    /// <typeparam name="TUPDATEDATA"></typeparam>ICommonTable
    public class CommonTable<TITEM, TUPDATEDATA> : ICommonTable<TUPDATEDATA> where TITEM : ICommonTable<TUPDATEDATA>
        // public class CommonTable<TITEM, TDATA, TUPDATEDATA> : ICommonTable<TUPDATEDATA> where TITEM : CommonTableItem<TDATA, TUPDATEDATA>
    {
        public List<TITEM> List { get; } = new List<TITEM>();

        public virtual void OnTurnStart(TUPDATEDATA data) => this.List.ForEach(p => p.OnTurnStart(data));
        public virtual void StartWork() => this.List.ForEach(p => p.StartWork());
        public virtual void StopWork() => this.List.ForEach(p => p.StopWork());

        public virtual float PhyImmunityFactor() => this.List.Aggregate(1.0f, (ret, item) => ret * item.PhyImmunityFactor());
        public virtual float PhyDamageFactor() => this.List.Aggregate(1.0f, (ret, item) => ret * item.PhyDamageFactor());
        // public virtual float LawImmunityFactor() => this.List.Aggregate(1, (ret, item) => ret * item.LawImmunityFactor());
        // public virtual float LawDamageFactor() => this.List.Aggregate(1, (ret, item) => ret * item.LawDamageFactor());
        // public virtual float HealFactor() => this.List.Aggregate(1, (ret, item) => ret * item.HealFactor());
        // public virtual float CreatureDurationTimeFactor() => this.List.Aggregate(1, (ret, item) => ret * item.CreatureDurationTimeFactor());
        // public virtual float DebuffDurationFactor() => this.List.Aggregate(1, (ret, item) => ret * item.DebuffDurationFactor());
        // public virtual float SkillIncreaseFactor() => this.List.Aggregate(1, (ret, item) => ret * item.SkillIncreaseFactor());
        // public virtual float SkillReduceFactor() => this.List.Aggregate(1, (ret, item) => ret * item.SkillReduceFactor());
        // public virtual float CanMove() => this.List.All(item => item.CanMove());
        // public virtual float CanDie() => this.List.All(item => item.CanDie());
        // public virtual float CanGetDamage() => this.List.All(item => item.CanGetDamage());
    }
}