using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class BuffData : BaseData
    {
        /// <summary>类型</summary>
        [System.ComponentModel.DisplayName("类型")]
        public int Type { get; private set; }

        /// <summary>常用效果</summary>
        [System.ComponentModel.DisplayName("常用效果")]
        public List<global::XSSLG.SkillEffectStruct> EffectArray { get; private set; }

        /// <summary>Stat</summary>
        [System.ComponentModel.DisplayName("Stat")]
        public long StatId { get; private set; }

        /// <summary>最大层数</summary>
        [System.ComponentModel.DisplayName("最大层数")]
        public int MaxCount { get; private set; }

        /// <summary>起始层数</summary>
        [System.ComponentModel.DisplayName("起始层数")]
        public int InitCount { get; private set; }

        /// <summary>Buff结束触发器Id</summary>
        [System.ComponentModel.DisplayName("Buff结束触发器Id")]
        public int FinishTriggerId { get; private set; }

        /// <summary>持续时间</summary>
        [System.ComponentModel.DisplayName("持续时间")]
        public int Duration { get; private set; }

        /// <summary>能否叠加</summary>
        [System.ComponentModel.DisplayName("能否叠加")]
        public bool CanStack { get; private set; }

        /// <summary>效果随层数变动</summary>
        [System.ComponentModel.DisplayName("效果随层数变动")]
        public int CanFloor { get; private set; }

        /// <summary>自定义数值数组</summary>
        [System.ComponentModel.DisplayName("自定义数值数组")]
        public List<global::XSSLG.PropStruct> PropArray { get; private set; }

        /// <summary>是否debuff</summary>
        [System.ComponentModel.DisplayName("是否debuff")]
        public bool IsDebuff { get; private set; }


        public static Int32 PID { get { return 0; } }

        // TODO 没有实现
        public StatData StatData { get; private set; }
    }

    public partial class BuffDataArray
    {
        public List<long> Keys { get; private set; }

        public List<global::XSSLG.BuffData> Items { get; private set; }

        public string TableHash { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

}
