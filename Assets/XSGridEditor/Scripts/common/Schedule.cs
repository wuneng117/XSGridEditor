using System;
using System.Collections.Generic;
namespace XSSLG
{
    // 定时器当前定时类型
    enum SchedulerStateType
    {
        None = 0,
        Delay = 1,  // 还在计算延时
        Interval = 2    // 开始计时
    }

    /// <summary>
    /// CustomScheduler的元素
    /// </summary>
    public class CustomSchedulerItem
    {
        public bool Active { get; protected set; } = false;
        protected float UpdateTime { get; set; } = 0;
        protected float Delay { get; set; } = 0;
        protected Action<CustomSchedulerItem> OnFinishHandler { get; set; }
        private Action Func { get; set; }
        private float Interval { get; set; } = 0;
        private int Repeat { get; set; } = -1;
        private SchedulerStateType State { get; set; } = SchedulerStateType.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func">回调</param>
        /// <param name="interval">间隔</param>
        /// <param name="repeat"></param>
        /// <param name="immediate">是否立即就调用</param>
        /// <param name="delay"></param>
        /// <param name="onFinishHandler"></param>
        public void Start(Action func, float interval, int repeat, bool immediate, float delay, Action<CustomSchedulerItem> onFinishHandler = null)
        {
            this.Active = true;
            this.Func = func;
            this.Interval = interval;
            this.Repeat = repeat;

            this.UpdateTime = (immediate && this.Delay == 0) ? this.Interval : 0;

            this.Delay = delay;
            this.State = this.Delay == 0 ? SchedulerStateType.Interval : SchedulerStateType.Delay;

            this.OnFinishHandler = onFinishHandler;      
        }

        public virtual void Stop()
        {
            this.Active = false;
            this.OnFinishHandler?.Invoke(this);
        }

        /// <summary>
        /// 自定义定时器的update要手动调用
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            if (this.Active == false)
                return;

            this.UpdateTime += dt;

            switch (this.State)
            {
                case SchedulerStateType.Delay: this.UpdateDelay(); break;
                case SchedulerStateType.Interval: this.UpdateInterval(); break;
                default: break;
            }
        }

        private void UpdateInterval()
        {
            if (this.UpdateTime < this.Interval)
                return;

            this.Func();
            this.UpdateTime -= this.Interval;
            if (this.Repeat > -1)
            {
                this.Repeat--;
                if (this.Repeat <= 0)
                    this.Stop();
            }
        }

        protected void UpdateDelay()
        {
            if (this.UpdateTime < this.Delay)
                return;

            this.UpdateTime -= this.Delay;
            this.State = SchedulerStateType.Interval;
        }
    }

    /// <summary>
    /// 不注释了，就是和ccscheduler一样的
    /// </summary>
    public class CustomScheduler
    {
        private List<CustomSchedulerItem> ScheduleList { get; } = new List<CustomSchedulerItem>();

        /// <summary>
        /// 定时调用func
        /// </summary>
        /// <param name="func">具体定时调用函数</param>
        /// <param name="interval">几回合调用一次</param>
        /// <param name="repeat">重复几次，为-1表示无限</param>
        /// <param name="immediate">是否立即调用一次</param>
        /// <param name="delay">延时几回合后再开始定时</param>
        /// <returns></returns>
        public CustomSchedulerItem Schedule(Action func, float interval, int repeat = -1, bool immediate = false, float delay = 0)
        {
            var scheduler = new CustomSchedulerItem();
            scheduler.Start(func, interval, repeat, immediate, delay, this.UnSchedule);
            this.ScheduleList.Add(scheduler);
            return scheduler;
        }

        /// <summary>
        /// 几回合后调用函数
        /// </summary>
        /// <param name="func">调用的函数</param>
        /// <param name="delay">几回合</param>
        /// <returns></returns>
        public CustomSchedulerItem ScheduleOnce(Action func, float delay) => this.Schedule(func, 0, 0, false, delay);

        /// <summary>
        /// 注销定时器
        /// </summary>
        /// <param name="schedule">定时器对象</param>
        public void UnSchedule(CustomSchedulerItem schedule) => this.ScheduleList.Remove((CustomSchedulerItem)schedule);

        /// <summary>
        /// 定时器更新
        /// 自定义定时器的update要手动调用
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            for(int index = this.ScheduleList.Count - 1; index >= 0; index--)
            {
                this.ScheduleList[index].Update(dt);
            }
        }

    }
}

