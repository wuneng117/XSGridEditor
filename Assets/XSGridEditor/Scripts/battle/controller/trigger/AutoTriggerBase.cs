using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary>
    /// 被动技能触发器，触发技能释放，或者buff和技能的结束触发
    /// 通过BattleEmitter注册触发事件，触发后自动调用Release施放技能
    /// 和主动技能的区别就是是否注册BattleEmitter，但是还有其他要注意的：
    /// 1.拥有这个trigger的技能必须是被动技能
    /// 2.triggerType不能是ClickCombat或者ClickMagic
    /// 3.因为第1，2条，参数OnTriggerDataBase不能是OnTriggerDataBase或者OntriggerDataCommon，因为这2者是给主动技能触发器用的，做好约束才能方便设计
    /// </summary>
    public class AutoTriggerBase : TriggerBase
    {
        /************************* 变量 begin ***********************/

        /// <summary> 释放时注销下事件 </summary>
        protected Action UnRegisterHander { get; set; }

        /************************* 变量  end  ***********************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data">触发器data</param>
        /// <param name="releaseEntity">触发器触发的对象</param>
        public AutoTriggerBase(TriggerData data, IReleaseEntity releaseEntity) : base(data, releaseEntity)
        {
            BattleEmitter.Instance.On(data.Type, (onTriggerData) => this.Release(onTriggerData));
            this.UnRegisterHander = () => BattleEmitter.Instance.Off(data.Type);
        }

        public override void StopWork()
        {
            base.StopWork();
            this.UnRegisterHander?.Invoke();
        }

        /// <summary>
        /// 获取处理对象
        /// </summary>
        /// <param name="data">触发数据</param>
        protected override List<UnitBase> GetTarget(OnTriggerDataBase data)
        {
            var ret = new List<UnitBase>();
            // 被动技能的OnTriggerData必须是OnTriggerData或子类，不能是OnTriggerDataBase，OnTriggerDataCommon
            var onTriggerData = data as OnTriggerData;
            if (onTriggerData == null)
                return ret;
            switch (this.Data.Condition.Target)
            {
                default:
                case XSDefine.TargetConditionType.None: break;
                case XSDefine.TargetConditionType.Src: ret.Add(onTriggerData.Src); break;
                case XSDefine.TargetConditionType.Dst: ret.AddRange(onTriggerData.Dst); break;
                case XSDefine.TargetConditionType.Search: ret.AddRange(this.SearchTarget.Search(onTriggerData)); break;
            }

            return ret;
        }
    }
}