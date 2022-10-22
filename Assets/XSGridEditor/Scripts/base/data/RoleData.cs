using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{

    [Serializable]
    public class RoleData : BaseData
    {
        [SerializeField]
        protected Texture2D texture;
        public Texture2D Texture { get => texture; set => texture = value; }

        /// <summary>初始等级</summary>
        [SerializeField]
        protected int lv;
        public int Lv { get => lv; set => lv = value; }

        /// <summary>当前初始职业</summary>
        [SerializeField]
        public string ClassDataName { get; set; }

        // public List<long> AbilityIDArray { get; private set; }

        // public List<long> CombatArtIDArray { get; private set; }

        // public List<long> LearnMagicIDArray { get; private set; }

        // /// <summary>技巧等级</summary>
        // public List<global::XSSLG.TechniqueLevel> TechniqueLvArray { get; private set; }


        public List<LearnSkillData> LearnSkillDataIDArray { get; private set; } = new List<LearnSkillData>();

        // /// <summary>行走图</summary>
        // public string Prefab { get; private set; }

        [SerializeField]
        protected Stat stat = new Stat();
        public Stat Stat { get => stat; protected set => stat = value; }

        [SerializeField]
        // /// <summary>纹章技能 SkillData</summary>
        public List<string> CrestKeyArray { get; private set; } = new List<string>();
        [SerializeField]
        // /// <summary>特技 SkillData</summary>
        public List<string> AbilityKeyArray { get; private set; } = new List<string>();
        [SerializeField]
        /// <summary>拥有的职业 ClassData</summary>
        public List<string> ClassDataKeyArray { get; private set; } = new List<string>();
        [SerializeField]
        // /// <summary>战技 SkillData</summary>
        public List<string> CombatArtKeyArray { get; private set; } = new List<string>();
        [SerializeField]
        // /// <summary>魔法 SkillData</summary>
        public List<string> MagicKeyArray { get; private set; } = new List<string>();
        [SerializeField]
        /// <summary>可以学会的技能 LearnSkillData</summary>
        public List<LearnSkillData> LearnSkillKeyArray { get; private set; } = new List<LearnSkillData>();
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
