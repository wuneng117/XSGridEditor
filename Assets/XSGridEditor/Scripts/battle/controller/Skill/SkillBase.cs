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
    public class SkillBase : CommonTableItem<SkillData, SkillUpdateData>, IReleaseEntity
    {
        /************************* 变量 begin ***********************/

        /// <summary> 触发器 </summary>
        public TriggerBase Trigger { get; protected set; }

        /// <summary> 属性值，给具体技能效果使用的数值 </summary>
        protected List<float> PropArray { get; set; } = new List<float>();

        /// <summary> 是否效果被其他技能抵消了 </summary>
        protected bool InvalidByOthers { get; set; } = false;

        /// <summary> 保留的玩家对象 </summary>
        public UnitBase Unit { get; protected set; }

        /// <summary> 被动技能加的属性 </summary>
        public Stat Stat { get; }

        /************************* 变量  end  ***********************/

        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SkillBase(SkillData data, UnitBase unit) : base(data)
        {
            this.Unit = unit;
            this.Trigger = TriggerFactory.CreateTrigger(data.TriggerData, this);
            this.Stat = new Stat(data.StatData);
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
            var ret = this.Data.EffectArray?.FindAll(effect => effect.Type == type).Aggregate(0f, (total, effect) => total + effect.Prop) ?? 0f;
            return ret;
        }

        public override bool GetSkillEffectFlag(SkillEffectType type)
        {
            var ret = this.Data.EffectArray?.Any(effect => effect.Type == type) ?? false;
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
            this.Data.BuffList?.ForEach(buffData => data.Target.ForEach(unit => this.AddBuff(buffData, unit)));
            return true;
        }

        /// <summary> 读取自定义属性 </summary>
        protected float GetProp(int index)
        {
            var propStruct = this.Data.PropArray?[index];
            if (propStruct == null)
                return 0;
            
            var ret = 0f;
            if (propStruct.Type.Length != 0)
            {
                var statProp = (float)(Stat.GetType().GetProperty(propStruct.Type)?.GetValue(this.Unit.GetStat()));
                ret = propStruct.Prop + propStruct.Percent * statProp;
            }
            else
                ret = propStruct.Prop;
                
            return 0;
        }

        /************************* buff相关 begin ***********************/
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
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SkillNull(SkillData data, UnitBase unit) : base(data, unit)
        {
            this.Trigger = TriggerFactory.CreateTriggerNull(this);
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

