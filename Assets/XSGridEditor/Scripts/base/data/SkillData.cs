
using System;
using System.Collections.Generic;

namespace XSSLG
{
    [Serializable]
    public partial class SkillData : BaseData
    {
        /// <summary>类型</summary>
        public SkillType Type = SkillType.Common;

        /// <summary>武器类型</summary>
        public WeaponType WeaponType;

        /// <summary>触发器id</summary>
        public string TriggerName;

        /// <summary>常用效果</summary>
        public List<SkillEffectStruct> EffectList = new List<SkillEffectStruct>();

        /// <summary>添加buffId数组</summary>
        public List<string> BuffKeyList = new List<string>();

        /// <summary> 技能存在时的一级属性加成, 类似被动技能 </summary>
        public Stat Stat = new Stat();
    }
}
