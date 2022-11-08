using System.Collections.Generic;
/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-11-08 21:23:14
/// @Description: 
/// </summary>
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
    public class TriggerManual : TriggerBase
    {
        /************************* 变量 begin ***********************/

        /************************* 变量  end  ***********************/
        public TriggerManual(TriggerData data, IReleaseEntity releaseEntity) : base(data, releaseEntity)
        {
        }

        /// <summary>
        /// 获取处理对象
        /// </summary>
        /// <param name="data">触发数据</param>
        protected override List<UnitBase> GetTarget(OnTriggerDataBase data)
        {
            var ret = new List<UnitBase>();
            ret.AddRange(this.SearchTarget.Search(data as OnTriggerDataCommon));
            return ret;
        }

    }
}