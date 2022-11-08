/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/30
/// @Description: 默认的触发器，不注册任何事件，手动调用触发时肯定可以触发，当技能data没有填触发器id时，技能默认用的这个
/// </summary>
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary>
    /// 默认的触发器，不注册任何事件，手动调用触发时肯定可以触发，当技能data没有填触发器id时，技能默认用的这个
    /// </summary>
    public class TriggerNull : TriggerBase
    {
        public TriggerNull(TriggerData data, IReleaseEntity releaseEntity) : base(data, releaseEntity)
        {
        }

        /// <summary>
        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override bool CanRelease(ReleaseData data) => true;

        /// <summary>
        /// 开始工作
        /// </summary>
        public override void StartWork() { }
        /// <summary>
        ///结束触发器，一般是卡牌离开场上结束
        /// </summary>
        public override void StopWork() { }

        protected override List<UnitBase> GetTarget(OnTriggerDataBase data) => new List<UnitBase>();
    }
}