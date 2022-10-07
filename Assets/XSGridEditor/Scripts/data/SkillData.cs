
using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class SkillData
    {
        /// <summary>技能ID</summary>
        [System.ComponentModel.DisplayName("技能ID")]
        public long Id { get; private set; }

        /// <summary>名字</summary>
        [System.ComponentModel.DisplayName("名字")]
        public string Name { get; private set; }

        /// <summary>描述</summary>
        [System.ComponentModel.DisplayName("描述")]
        public string Desc { get; private set; }

        /// <summary>类型</summary>
        [System.ComponentModel.DisplayName("类型")]
        public global::XSSLG.SkillDataSkillType Type { get; private set; }

        /// <summary>技能分组</summary>
        [System.ComponentModel.DisplayName("技能分组")]
        public global::XSSLG.SkillDataSkillGroupType Group { get; private set; }

        /// <summary>武器类型</summary>
        [System.ComponentModel.DisplayName("武器类型")]
        public global::XSSLG.WeaponType WeaponType { get; private set; }

        /// <summary>触发器id</summary>
        [System.ComponentModel.DisplayName("触发器id")]
        public long TriggerId { get; private set; }

        /// <summary>常用效果</summary>
        [System.ComponentModel.DisplayName("常用效果")]
        public List<global::XSSLG.SkillEffectStruct> EffectArray { get; private set; }

        /// <summary>添加buffId数组</summary>
        [System.ComponentModel.DisplayName("添加buffId数组")]
        public List<long> BuffIdArray { get; private set; }

        /// <summary>Stat</summary>
        [System.ComponentModel.DisplayName("Stat")]
        public long StatId { get; private set; }

        /// <summary>自定义数值数组</summary>
        [System.ComponentModel.DisplayName("自定义数值数组")]
        public List<global::XSSLG.PropStruct> PropArray { get; private set; }


        public static Int32 PID { get { return 0; } }

        //TODO 未实现
        public TriggerData TriggerData { get; private set; }
        public StatData StatData { get; private set; }
        public List<BuffData> BuffList { get; private set; }
    }

    public partial class SkillDataArray
    {
        public List<long> Keys { get; private set; }

        public List<global::XSSLG.SkillData> Items { get; private set; }

        public string TableHash { get; private set; }


        public static Int32 PID { get { return 0; } }
    }

    public enum SkillDataSkillGroupType
    {
        Normal = 0,
        /// <summary>被动技能</summary>
        [System.ComponentModel.Description("被动技能")]
        Passive = 1,
        /// <summary>纹章技能</summary>
        [System.ComponentModel.Description("纹章技能")]
        Crest = 2,
    }


    public enum SkillDataSkillType
    {
        Common = 0,
        /// <summary>战斗技能</summary>
        [System.ComponentModel.Description("战斗技能")]
        Combat = 1,
        /// <summary>魔法主动技能</summary>
        [System.ComponentModel.Description("魔法主动技能")]
        Magic = 2,
    }


}
