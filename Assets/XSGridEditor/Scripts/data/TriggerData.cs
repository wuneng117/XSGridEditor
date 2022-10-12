
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public partial class ConditionStruct
    {

        /// <summary>条件类型</summary>
        [SerializeField]
        protected XSDefine.TriggerConditionType type;

        public XSDefine.TriggerConditionType Type { get => type; protected set => type = value; }

        /// <summary>数值</summary>
        [SerializeField]
        protected float prop;

        public float Prop { get => prop; protected set => prop = value; }

        /// <summary>比较类型</summary>
        [SerializeField]
        protected CompareType compare;
        public CompareType Compare { get => compare; protected set => compare = value; }

        /// <summary>条件对象类型</summary>
        [SerializeField]
        protected XSDefine.TargetConditionType target;
        public XSDefine.TargetConditionType Target { get => target; protected set => target = value; }
    }

    [Serializable]
    public class SearchStruct
    {

        [SerializeField]
        /// <summary>目标类型</summary>
        public List<XSDefine.SearchTargetType> Target { get; protected set; } = new List<XSDefine.SearchTargetType>();

        /// <summary>索敌类型</summary>
        [SerializeField]
        protected XSDefine.SearchType type;

        public XSDefine.SearchType Type { get => type; protected set => type = value; }

        /// <summary>最小范围</summary>
        [SerializeField]
        protected int min;

        public int Min { get => min; protected set => min = value; }

        /// <summary>最大范围</summary>
        [SerializeField]
        protected int max;
        public int Max { get => max; protected set => max = value; }
    }

    [Serializable]
    public class TriggerData : BaseData
    {

        /// <summary>类型</summary>
        [SerializeField]
        protected XSDefine.TriggerType type;

        public XSDefine.TriggerType Type { get => type; set => type = value; }

        /// <summary>特殊类型</summary>
        [SerializeField]
        protected XSDefine.TriggerSpecialType specialType;

        public XSDefine.TriggerSpecialType SpecialType { get => specialType; set => specialType = value; }

        [SerializeField]
        /// <summary>条件</summary>
        public ConditionStruct Condition { get; protected set; } = new ConditionStruct();

        [SerializeField]
        /// <summary>索敌</summary>
        public SearchStruct SearchTarget { get; protected set; } = new SearchStruct();

        /// <summary>CD</summary>
        [SerializeField]
        protected int cd;

        public int Cd { get => cd; protected set => cd = value; }

        /// <summary>是否开始就cd</summary>
        [SerializeField]
        protected bool isCdOnStart;
        public bool IsCdOnStart { get => isCdOnStart; protected set => isCdOnStart = value; }
    }
}
