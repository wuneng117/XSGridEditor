namespace XSSLG
{
    public partial class XSDefine
    {
        public enum ClassLvType
        {
            /// <summary>基础</summary>
            [System.ComponentModel.Description("基础")]
            Unique = 0,
            Beginner = 1,
            /// <summary>中级</summary>
            [System.ComponentModel.Description("中级")]
            Intermediate = 2,
            /// <summary>高级</summary>
            [System.ComponentModel.Description("高级")]
            Advanced = 3,
            /// <summary>大师</summary>
            [System.ComponentModel.Description("大师")]
            Master = 4,
            Max,
        }

        public enum TechniqueType
        {
            /// <summary>剑</summary>
            [System.ComponentModel.Description("剑")]
            Sword = 0,
            /// <summary>枪</summary>
            [System.ComponentModel.Description("枪")]
            Lance = 1,
            /// <summary>斧</summary>
            [System.ComponentModel.Description("斧")]
            Axe = 2,
            /// <summary>弓</summary>
            [System.ComponentModel.Description("弓")]
            Bow = 3,
            /// <summary>拳套</summary>
            [System.ComponentModel.Description("拳套")]
            Brawl = 4,
            /// <summary>理学</summary>
            [System.ComponentModel.Description("理学")]
            Reason = 5,
            /// <summary>信仰</summary>
            [System.ComponentModel.Description("信仰")]
            Faith = 6,
            /// <summary>指挥</summary>
            [System.ComponentModel.Description("指挥")]
            Authority = 7,
            /// <summary>重装</summary>
            [System.ComponentModel.Description("重装")]
            HvyArmor = 8,
            /// <summary>马术</summary>
            [System.ComponentModel.Description("马术")]
            Riding = 9,
            /// <summary>飞行</summary>
            [System.ComponentModel.Description("飞行")]
            Flying = 10,
            Max,
        }
    }
}