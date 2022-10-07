using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class RoleData : BaseData
    {
        /// <summary>初始等级</summary>
        [SerializeField]
        protected int lv;
        [System.ComponentModel.DisplayName("初始等级")]
        public int Lv { get => lv; set => lv = value; }

        /// <summary>当前初始职业</summary>
        [SerializeField]
        [System.ComponentModel.DisplayName("当前初始职业")]
        public string ClassDataName { get; set; }

        // /// <summary>特技</summary>
        // [System.ComponentModel.DisplayName("特技")]
        // public List<long> AbilityIDArray { get; private set; }

        // /// <summary>战技</summary>
        // [System.ComponentModel.DisplayName("战技")]
        // public List<long> CombatArtIDArray { get; private set; }

        // /// <summary>魔法</summary>
        // [System.ComponentModel.DisplayName("魔法")]
        // public List<long> LearnMagicIDArray { get; private set; }

        // /// <summary>技巧等级</summary>
        // [System.ComponentModel.DisplayName("技巧等级")]
        // public List<global::XSSLG.TechniqueLevel> TechniqueLvArray { get; private set; }

        // /// <summary>纹章技能</summary>
        // [System.ComponentModel.DisplayName("纹章技能")]
        // public List<long> CrestIDArray { get; private set; }

        // /// <summary>拥有的职业</summary>
        // [System.ComponentModel.DisplayName("拥有的职业")]
        // public List<long> ClassDataIDArray { get; private set; }

        // /// <summary>学会的技能</summary>
        // [System.ComponentModel.DisplayName("学会的技能")]
        // public List<long> LearnSkillDataIDArray { get; private set; }

        // /// <summary>行走图</summary>
        // [System.ComponentModel.DisplayName("行走图")]
        // public string Prefab { get; private set; }

        [SerializeField]
        protected Stat stat = new Stat();
        public Stat Stat { get => stat; protected set => stat = value; }
        
        // public List<SkillData> CrestArray { get; private set; }
        // public List<SkillData> AbilityArray { get; private set; }
        /// <summary> 学会的职业 </summary>
        [SerializeField]
        public List<string> ClassDataArray { get; private set; } = new List<string>();
        // public List<SkillData> CombatArtArray { get; private set; }
        // public List<SkillData> LearnMagicArray { get; private set; }
        // public List<SkillData> LearnSkillDataArray { get; private set; }
    }
}
