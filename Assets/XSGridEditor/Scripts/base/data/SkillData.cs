
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public partial class SkillData : BaseData
    {

        protected SkillType type;

        [SerializeField]
        /// <summary>类型</summary>
        public SkillType Type { get => type; set => type = value; }

        [SerializeField]
        protected SkillGroupType group;

        /// <summary>技能分组</summary>
        public SkillGroupType Group { get => group; set => group = value; }

        [SerializeField]
        private WeaponType weaponType;
        /// <summary>武器类型</summary>
        public WeaponType WeaponType { get => weaponType; set => weaponType = value; }

        [SerializeField]
        /// <summary>触发器id</summary>
        public string TriggerName { get; set; }

        [SerializeField]
        /// <summary>常用效果</summary>
        public List<SkillEffectStruct> EffectList { get; private set; } = new List<SkillEffectStruct>();

        [SerializeField]
        /// <summary>添加buffId数组</summary>
        public List<string> BuffKeyList { get; private set; } = new List<string>();

        /// <summary> 技能存在时的一级属性加成, 类似被动技能 </summary>
        [SerializeField]
        public Stat Stat { get; set; } = new Stat();
    }
}
