using System;
using System.Linq;

namespace XSSLG
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/19
/// @Description: buff基类
/// </summary>
{
    /// <summary> buff基类 </summary>
    public class BuffBase : CommonTableItem<BuffData, SkillUpdateData>
    {

        /// <summary> 这个buff的宿主skill，如果不是特定的，就是加在unit的skillnull对象里 </summary>
        public SkillBase Skill { get; }

        /// <summary> buff加的属性 </summary>
        public Stat Stat { get; }

        /// <summary> buff层数 </summary>
        private int _count = 0;
        private int TimeLeft { get; set; } = 0;

        public int Count
        {
            get => _count; protected set
            {
                if (value <= 0)
                    return;

                this._count += value;
                this._count = Math.Min(this._count, this.Data.MaxCount);
            }
        }

        /// <summary> 是否有效，在buff die的时候设置为false，如果再执行到die（die事件触发），为true就不处理，可以避免死循环 </summary>
        private bool IsAlive { get; set; } = true;

        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BuffBase(BuffData data, SkillBase skill) : base(data)
        {
            this.Skill = skill;
            // data TODO
            // this.Stat = new Stat(data.StatData);
        }
        // virtual
        public void Destroy()
        {
            // this.regOffEvent();
            // this._skill.emitSkillEffectTime(EffectTime.OnBuffFinish);
            //     this._skill.getEmitter().emit(TriggerBasicType.OnSelfBuffDied);
            //     this.emitBuffEffectTime(EffectTime.OnBuffFinish);

            //     this._effectArray.forEach((effect) => {
            //         effect.destroy();
            //     });
        }

        public override float GetSkillEffectProp(SkillEffectType type)
        {
            var ret = this.Data.EffectArray.FindAll(effect => effect.Type == type).Aggregate(0f, (total, effect) => total + effect.Prop);
            return ret;
        }

        public override bool GetSkillEffectFlag(SkillEffectType type)
        {
            var ret = this.Data.EffectArray.Any(effect => effect.Type == type);
            return ret;
        }

        public override void StartWork() { }
        public override void StopWork() { }

        /// <summary> 增加buff层数 </summary>
        public void AddCount(int Count = 1)
        {
            if (!this.IsAlive)
                return;

            this.Count += Count;

            //TODO 特效什么的？
        }

        public virtual void OnAdd()
        {
            if (this.NeedCountDuration())
            {
                this.TimeLeft = this.Data.Duration;
                // if (d.isDebuff)
                // {
                //     this._timeLeft *= this._top.buffTable.getDebuffDurationFactor();
                // }
            }
            else
                this.TimeLeft = XSDefine.MAX_BUFF_DURATION;
        }

        /// <summary> 是否需要持续时间计算 </summary>
        private bool NeedCountDuration() => this.Data.Duration != 0;
    }
}