
using System;
using System.Collections.Generic;

namespace XSSLG
{
    [Serializable]
    public partial class ConditionStruct
    {

        /// <summary>条件类型</summary>
        public TriggerConditionType Type;

        /// <summary>数值</summary>
        public float Prop;

        /// <summary>比较类型</summary>
        public CompareType Compare;
    }

    [Serializable]
    public class SearchStruct
    {

        /// <summary>目标类型</summary>
        public List<SearchTargetType> Target = new List<SearchTargetType>();

        /// <summary>索敌类型</summary>
        public SearchType Type;

        /// <summary>最小范围</summary>
        public int Min;

        /// <summary>最大范围</summary>
        public int Max;
    }

    [Serializable]
    public class TriggerData : BaseData
    {

        /// <summary>类型</summary>
        public TriggerType Type;

        /// <summary>条件</summary>
        public List<ConditionStruct> ConditionList = new List<ConditionStruct>();

        /// <summary>索敌</summary>
        public SearchStruct SearchTarget = new SearchStruct();

        /// <summary>条件对象类型</summary>
        public TargetConditionType Target;

        /// <summary>CD</summary>
        public int CD;

        /// <summary>是否开始就cd</summary>
        public bool IsCDOnStart;
    }
}
