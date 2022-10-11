﻿using System;
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
        public int Lv { get => lv; set => lv = value; }

        /// <summary>当前初始职业</summary>
        [SerializeField]
        public string ClassDataName { get; set; }

        // /// <summary>特技</summary>
        // public List<long> AbilityIDArray { get; private set; }

        // /// <summary>战技</summary>
        // public List<long> CombatArtIDArray { get; private set; }

        // /// <summary>魔法</summary>
        // public List<long> LearnMagicIDArray { get; private set; }

        // /// <summary>技巧等级</summary>
        // public List<global::XSSLG.TechniqueLevel> TechniqueLvArray { get; private set; }

        // /// <summary>纹章技能</summary>
        // public List<long> CrestIDArray { get; private set; }

        // /// <summary>拥有的职业</summary>
        // public List<long> ClassDataIDArray { get; private set; }

        /// <summary>可以学会的技能</summary>
        public List<LearnSkillData> LearnSkillDataIDArray { get; private set; } = new List<LearnSkillData>();

        // /// <summary>行走图</summary>
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

    [Serializable]
    public class LearnSkillData
    {
        /// <summary>名字</summary>
        [SerializeField]
        public string Name { get; set; }

        /// <summary>需要角色等级</summary>
        [SerializeField]
        public int NeedLv { get; set; }

        /// <summary>需要技巧等级</summary>
        [SerializeField]
        public TechniqueLevel TechnieuqLv { get; set; }

        /// <summary>学会技能ID</summary>
        [SerializeField]
        public string SkillName { get; set; }
    }
}
