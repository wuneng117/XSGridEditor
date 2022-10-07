
using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class TriggerDataConditionStruct
    {
        /// <summary>条件类型</summary>
        [System.ComponentModel.DisplayName("条件类型")]
        public global::XSSLG.TriggerDataConditionType Type { get; private set; }

        /// <summary>数值</summary>
        [System.ComponentModel.DisplayName("数值")]
        public float Prop { get; private set; }

        /// <summary>比较类型</summary>
        [System.ComponentModel.DisplayName("比较类型")]
        public global::XSSLG.CompareType Compare { get; private set; }

        /// <summary>条件对象类型</summary>
        [System.ComponentModel.DisplayName("条件对象类型")]
        public global::XSSLG.TriggerDataConditionTargetType Target { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class TriggerDataSearchStruct
    {
        /// <summary>目标类型</summary>
        [System.ComponentModel.DisplayName("目标类型")]
        public List<global::XSSLG.TriggerDataSearchTargetType> Target { get; private set; }

        /// <summary>索敌类型</summary>
        [System.ComponentModel.DisplayName("索敌类型")]
        public global::XSSLG.TriggerDataSearchType Type { get; private set; }

        /// <summary>最小范围</summary>
        [System.ComponentModel.DisplayName("最小范围")]
        public int Min { get; private set; }

        /// <summary>最大范围</summary>
        [System.ComponentModel.DisplayName("最大范围")]
        public int Max { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class TriggerData
    {
        /// <summary>触发器ID</summary>
        [System.ComponentModel.DisplayName("触发器ID")]
        public long Id { get; private set; }

        /// <summary>名字</summary>
        [System.ComponentModel.DisplayName("名字")]
        public string Name { get; private set; }

        /// <summary>描述</summary>
        [System.ComponentModel.DisplayName("描述")]
        public string Desc { get; private set; }

        /// <summary>类型</summary>
        [System.ComponentModel.DisplayName("类型")]
        public global::XSSLG.TriggerDataTriggerType Type { get; private set; }

        /// <summary>特殊类型</summary>
        [System.ComponentModel.DisplayName("特殊类型")]
        public global::XSSLG.TriggerDataTriggerSpecialType SpecialType { get; private set; }

        /// <summary>条件</summary>
        [System.ComponentModel.DisplayName("条件")]
        public global::XSSLG.TriggerDataConditionStruct Condition { get; private set; }

        /// <summary>索敌</summary>
        [System.ComponentModel.DisplayName("索敌")]
        public global::XSSLG.TriggerDataSearchStruct SearchTarget { get; private set; }

        /// <summary>CD</summary>
        [System.ComponentModel.DisplayName("CD")]
        public int Cd { get; private set; }

        /// <summary>是否开始就cd</summary>
        [System.ComponentModel.DisplayName("是否开始就cd")]
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
        [System.ComponentModel.Description("对应releasedata的src")]
        Src = 1,
        /// <summary>对应releasedata的dst</summary>
        [System.ComponentModel.Description("对应releasedata的dst")]
        Dst = 2,
        /// <summary>索敌出来的target</summary>
        [System.ComponentModel.Description("索敌出来的target")]
        Search = 3,
    }


    public enum TriggerDataConditionType
    {
        None = 0,
        /// <summary>自己造成的伤害</summary>
        [System.ComponentModel.Description("自己造成的伤害")]
        SelfCauseDamage = 1,
    }


    public enum TriggerDataSearchTargetType
    {
        None = 0,
        Self = 1,
        /// <summary>友方不包括自己</summary>
        [System.ComponentModel.Description("友方不包括自己")]
        Friend = 2,
        Enemy = 3,
    }


    public enum TriggerDataSearchType
    {
        None = 0,
        /// <summary>前方1格</summary>
        [System.ComponentModel.Description("前方1格")]
        Front1 = 1,
        /// <summary>前方1X2</summary>
        [System.ComponentModel.Description("前方1X2")]
        Front1x2 = 2,
        /// <summary>前方1X3</summary>
        [System.ComponentModel.Description("前方1X3")]
        Front1x3 = 3,
        /// <summary>前方3X1</summary>
        [System.ComponentModel.Description("前方3X1")]
        Front3x1 = 4,
        /// <summary>范围内1格</summary>
        [System.ComponentModel.Description("范围内1格")]
        Scope1 = 5,
        /// <summary>小十字</summary>
        [System.ComponentModel.Description("小十字")]
        ScopeCross1 = 6,
        /// <summary>十字</summary>
        [System.ComponentModel.Description("十字")]
        ScopeCross2 = 7,
        /// <summary>大十字</summary>
        [System.ComponentModel.Description("大十字")]
        ScopeCross3 = 8,
    }


    public enum TriggerDataTriggerSpecialType
    {
        None = 0,
    }


    public enum TriggerDataTriggerType
    {
        /// <summary>没用</summary>
        [System.ComponentModel.Description("没用")]
        Common = 0,
        /// <summary>回合开始时</summary>
        [System.ComponentModel.Description("回合开始时")]
        OnTurnStart = 1,
        /// <summary>战斗后</summary>
        [System.ComponentModel.Description("战斗后")]
        AfterAttack = 2,
        /// <summary>点击释放战技/普通攻击</summary>
        [System.ComponentModel.Description("点击释放战技/普通攻击")]
        ClickCombat = 3,
        /// <summary>点击释放魔法</summary>
        [System.ComponentModel.Description("点击释放魔法")]
        ClickMagic = 4,
    }


}
