/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/31
/// @Description: 技能基类，啊啊啊 
/// </summary>
using System.Collections.Generic;
using System.Linq;

namespace XSSLG
{
    /// <summary> 技能基类 </summary>
    public abstract class SkillBase : CommonTableItem<SkillData, SkillUpdateData>, IReleaseEntity
    {
        /************************* 变量 begin ***********************/
        /// <summary> 技能类型 </summary>
        public SkillType SkillType => this.Data.Type;

        /// <summary> 触发器 </summary>
        public abstract TriggerBase Trigger { get; }

        /// <summary> 属性值，给具体技能效果使用的数值 </summary>
        protected List<float> PropArray { get; set; } = new List<float>();

        /// <summary> 是否效果被其他技能抵消了 </summary>
        protected bool InvalidByOthers { get; set; } = false;

        /// <summary> 保留的玩家对象 </summary>
        public UnitBase Unit { get; protected set; }

        public virtual Stat Stat { get => this.Data.Stat; }

        /************************* 变量  end  ***********************/

        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SkillBase(SkillData data, UnitBase unit) : base(data)
        {
            this.Unit = unit;
            // this.Trigger = TriggerFactory.CreateTrigger(data.TriggerName, this);
        }

        public override void StartWork()
        {
            this.Trigger.StartWork();
        }
        public override void StopWork()
        {
            this.Trigger.StopWork();
        }

        public override void OnTurnStart(SkillUpdateData data)
        {
            base.OnTurnStart(data);
            this.Trigger.OnTurnStart(data);
        }

        public override float GetSkillEffectProp(SkillEffectType type)
        {
            var ret = this.Data.EffectList?.FindAll(effect => effect.Type == type).Aggregate(0f, (total, effect) => total + effect.Prop) ?? 0f;
            return ret;
        }

        public override bool GetSkillEffectFlag(SkillEffectType type)
        {
            var ret = this.Data.EffectList?.Any(effect => effect.Type == type) ?? false;
            return ret;
        }

        /// <summary>
        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool CanRelease(ReleaseData data)
        {
            //TODO 次数

            return true;
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="data"></param>
        public virtual bool Release(ReleaseData data)
        {
            this.InvalidByOthers = false;
            data.OnTriggerData.Chain.Add(this);   // 很牛逼的一个链条
            // 加buff
            this.Data.BuffKeyList?.ForEach(name => data.Target.ForEach(unit => this.AddBuff(name, unit)));
            return true;
        }

        /************************* buff相关 begin ***********************/
        public BuffBase AddBuff(string name, UnitBase targetUnit)
        {
            var buff = BuffFactory.CreateBuff(name, this);
            if (buff == null)
                return null;

            targetUnit.Table.BuffTable.Add(buff);
            return buff;
        }

        public BuffBase AddBuff(BuffData data, UnitBase targetUnit)
        {
            var buff = BuffFactory.CreateBuff(data, this);
            if (buff == null)
                return null;

            targetUnit.Table.BuffTable.Add(buff);
            return buff;
        }

        /************************* buff相关  end  ***********************/
    }

    /// <summary>
    /// 默认的技能，给CardBase.MainSkill使用，这样就不用去判断是否为null了
    /// </summary>
    public class SkillNull : SkillBase
    {
        protected TriggerNull trigger;
        public override TriggerBase Trigger => trigger;
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SkillNull(SkillData data, UnitBase unit) : base(data, unit)
        {
            this.trigger = TriggerFactory.CreateTriggerNull(this);
        }

        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool CanRelease(ReleaseData data) => true;

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="data"></param>
        public override bool Release(ReleaseData data) => true;
    }
}

