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

        public enum TargetConditionType
        {
            None = 0,
            /// <summary>对应releasedata的src</summary>
            Src = 1,
            /// <summary>对应releasedata的dst</summary>
            Dst = 2,
            /// <summary>索敌出来的target</summary>
            Search = 3,
            Max,
        }


        public enum TriggerConditionType
        {
            None = 0,
            /// <summary>自己造成的伤害</summary>
            SelfCauseDamage = 1,
            Max,
        }


        public enum SearchTargetType
        {
            None = 0,
            Self = 1,
            /// <summary>友方不包括自己</summary>
            Friend = 2,
            Enemy = 3,
            Max,
        }


        public enum SearchType
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
            Max,
        }


        public enum TriggerSpecialType
        {
            None = 0,
            Max,
        }


        public enum TriggerType
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
            Max,
        }
    }
}