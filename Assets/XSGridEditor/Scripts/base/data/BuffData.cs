using System;
using System.Collections.Generic;

namespace XSSLG
{
    [Serializable]
    public partial class BuffData : BaseData
    {

        /// <summary>类型</summary>
        public int Type;

        /// <summary>常用效果</summary>
        public List<SkillEffectStruct> EffectList = new List<SkillEffectStruct>();

        /// <summary>最大层数</summary>
        public int MaxCount;

        /// <summary>起始层数</summary>
        public int InitCount;

        /// <summary>Buff结束触发器Id</summary>
        public int FinishTriggerId;

        /// <summary>持续时间</summary>
        public int Duration;

        /// <summary>能否叠加</summary>
        public bool CanStack;

        /// <summary>效果随层数变动</summary>
        public int CanFloor;

        /// <summary>自定义数值数组</summary>
        public List<PropStruct> PropList = new List<PropStruct>();

        /// <summary>是否debuff</summary>
        public bool IsDebuff;

        /// <summary> buff存在时的一级属性加成 </summary>
        public Stat Stat;
    }
}
