namespace XSSLG
{
    public partial class XSDefine
    {
        public enum ClassLvType
        {
            /// <summary>基础</summary>
            Unique = 0,
            Beginner = 1,
            /// <summary>中级</summary>
            Intermediate = 2,
            /// <summary>高级</summary>
            Advanced = 3,
            /// <summary>大师</summary>
            Master = 4,
            Max,
        }

        public enum TechniqueType
        {
            /// <summary>剑</summary>
            Sword = 0,
            /// <summary>枪</summary>
            Lance = 1,
            /// <summary>斧</summary>
            Axe = 2,
            /// <summary>弓</summary>
            Bow = 3,
            /// <summary>拳套</summary>
            Brawl = 4,
            /// <summary>理学</summary>
            Reason = 5,
            /// <summary>信仰</summary>
            Faith = 6,
            /// <summary>指挥</summary>
            Authority = 7,
            /// <summary>重装</summary>
            HvyArmor = 8,
            /// <summary>马术</summary>
            Riding = 9,
            /// <summary>飞行</summary>
            Flying = 10,
            Max,
        }


        public enum SkillGroupType
        {
            Normal = 0,
            /// <summary>被动技能</summary>
            Passive = 1,
            /// <summary>纹章技能</summary>
            Crest = 2,
            Max,
        }

        public enum SkillType
        {
            Common = 0,
            /// <summary>战斗技能</summary>
            Combat = 1,
            /// <summary>魔法主动技能</summary>
            Magic = 2,
            Max,
        }
    }
}