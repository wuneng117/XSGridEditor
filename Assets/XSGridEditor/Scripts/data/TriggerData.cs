
using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class TriggerDataConditionStruct
    {
        /// <summary>条件类型</summary>
        public global::XSSLG.TriggerDataConditionType Type { get; private set; }

        /// <summary>数值</summary>
        public float Prop { get; private set; }

        /// <summary>比较类型</summary>
        public global::XSSLG.CompareType Compare { get; private set; }

        /// <summary>条件对象类型</summary>
        public global::XSSLG.TriggerDataConditionTargetType Target { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class TriggerDataSearchStruct
    {
        /// <summary>目标类型</summary>
        public List<global::XSSLG.TriggerDataSearchTargetType> Target { get; private set; }

        /// <summary>索敌类型</summary>
        public global::XSSLG.TriggerDataSearchType Type { get; private set; }

        /// <summary>最小范围</summary>
        public int Min { get; private set; }

        /// <summary>最大范围</summary>
        public int Max { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class TriggerData : BaseData
    {
        /// <summary>类型</summary>
        public global::XSSLG.TriggerDataTriggerType Type { get; private set; }

        /// <summary>特殊类型</summary>
        public global::XSSLG.TriggerDataTriggerSpecialType SpecialType { get; private set; }

        /// <summary>条件</summary>
        public global::XSSLG.TriggerDataConditionStruct Condition { get; private set; }

        /// <summary>索敌</summary>
        public global::XSSLG.TriggerDataSearchStruct SearchTarget { get; private set; }

        /// <summary>CD</summary>
        public int Cd { get; private set; }

        /// <summary>是否开始就cd</summary>
        public bool IsCdOnStart { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class TriggerDataArray
    {
        public List<long> Keys { get; private set; }

        public List<global::XSSLG.TriggerData> Items { get; private set; }

        public string TableHash { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public enum TriggerDataConditionTargetType
    {
        None = 0,
        /// <summary>对应releasedata的src</summary>
        Src = 1,
        /// <summary>对应releasedata的dst</summary>
        Dst = 2,
        /// <summary>索敌出来的target</summary>
        Search = 3,
    }


    public enum TriggerDataConditionType
    {
        None = 0,
        /// <summary>自己造成的伤害</summary>
        SelfCauseDamage = 1,
    }


    public enum TriggerDataSearchTargetType
    {
        None = 0,
        Self = 1,
        /// <summary>友方不包括自己</summary>
        Friend = 2,
        Enemy = 3,
    }


    public enum TriggerDataSearchType
    {
        None = 0,
        /// <summary>前方1格</summary>
        Front1 = 1,
        /// <summary>前方1X2</summary>
        Front1x2 = 2,
        /// <summary>前方1X3</summary>
        Front1x3 = 3,
        /// <summary>前方3X1</summary>
        Front3x1 = 4,
        /// <summary>范围内1格</summary>
        Scope1 = 5,
        /// <summary>小十字</summary>
        ScopeCross1 = 6,
        /// <summary>十字</summary>
        ScopeCross2 = 7,
        /// <summary>大十字</summary>
        ScopeCross3 = 8,
    }


    public enum TriggerDataTriggerSpecialType
    {
        None = 0,
    }


    public enum TriggerDataTriggerType
    {
        /// <summary>没用</summary>
        Common = 0,
        /// <summary>回合开始时</summary>
        OnTurnStart = 1,
        /// <summary>战斗后</summary>
        AfterAttack = 2,
        /// <summary>点击释放战技/普通攻击</summary>
        ClickCombat = 3,
        /// <summary>点击释放魔法</summary>
        ClickMagic = 4,
    }


}
