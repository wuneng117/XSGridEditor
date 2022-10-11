using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public partial class BuffData : BaseData
    {

        /// <summary>类型</summary>
        [SerializeField]
        public int Type { get; set; }

        /// <summary>常用效果</summary>
        [SerializeField]
        public List<SkillEffectStruct> EffectArray { get; protected set; } = new List<SkillEffectStruct>();

        /// <summary>最大层数</summary>
        [SerializeField]
        protected int maxCount;
        public int MaxCount { get => maxCount; set => maxCount = value; }

        /// <summary>起始层数</summary>
        [SerializeField]
        protected int initCount;
        public int InitCount { get => initCount; set => initCount = value; }

        /// <summary>Buff结束触发器Id</summary>
        [SerializeField]
        public int FinishTriggerId { get; set; }

        /// <summary>持续时间</summary>
        [SerializeField]
        protected int duration;
        public int Duration { get => duration; set => duration = value; }

        /// <summary>能否叠加</summary>
        [SerializeField]
        protected bool canStack;
        public bool CanStack { get => canStack; set => canStack = value; }

        /// <summary>效果随层数变动</summary>
        [SerializeField]
        protected int canFloor;
        public int CanFloor { get => canFloor; set => canFloor = value; }

        /// <summary>自定义数值数组</summary>
        [SerializeField]
        public List<PropStruct> PropArray { get; protected set; } = new List<PropStruct>();

        /// <summary>是否debuff</summary>
        [SerializeField]
        protected bool isDebuff;
        public bool IsDebuff { get => isDebuff; set => isDebuff = value; }

        /// <summary> buff存在时的一级属性加成 </summary>
        [SerializeField]
        public StatData StatData { get; set; }
    }
}
