
using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class SkillData
    {
        /// <summary>名字</summary>
        public string Name { get; set; }

        /// <summary>描述</summary>
        public string Desc { get; set; }

        /// <summary>类型</summary>
        public global::XSSLG.SkillDataSkillType Type { get; set; }

        /// <summary>技能分组</summary>
        public global::XSSLG.SkillDataSkillGroupType Group { get; set; }

        /// <summary>武器类型</summary>
        public global::XSSLG.WeaponType WeaponType { get; set; }

        /// <summary>触发器id</summary>
        public long TriggerId { get; set; }

        /// <summary>常用效果</summary>
        public List<global::XSSLG.SkillEffectStruct> EffectArray { get; private set; }

        /// <summary>添加buffId数组</summary>
        public List<long> BuffIdArray { get; private set; }

        /// <summary> 技能存在时的一级属性加成, 类似被动技能 </summary>
        [SerializeField]
        public StatData StatData { get; set; }

        /// <summary>自定义数值数组</summary>
        public List<global::XSSLG.PropStruct> PropArray { get; private set; }

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
        Passive = 1,
        /// <summary>纹章技能</summary>
        Crest = 2,
    }


    public enum SkillDataSkillType
    {
        Common = 0,
        /// <summary>战斗技能</summary>
        Combat = 1,
        /// <summary>魔法主动技能</summary>
        Magic = 2,
    }


}
