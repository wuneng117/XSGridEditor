using System;
using System.Collections.Generic;

namespace XSSLG
{

    [Serializable]
    public class RoleData : BaseData
    {
        /// <summary>初始等级</summary>
        public int Lv;

        /// <summary>当前初始职业</summary>
        public string ClassDataKey;

        // public List<long> AbilityIDArray { get; private set; }

        // public List<long> CombatArtIDArray { get; private set; }

        // public List<long> LearnMagicIDArray { get; private set; }

        // /// <summary>技巧等级</summary>
        // public List<global::XSSLG.TechniqueLevel> TechniqueLvArray { get; private set; }

        // /// <summary>行走图</summary>
        // public string Prefab { get; private set; }

        public Stat Stat = new Stat();

        // /// <summary>纹章技能 SkillData</summary>
        public List<string> CrestKeyList = new List<string>();
        
        // /// <summary>特技 SkillData</summary>
        public List<string> AbilityKeyList = new List<string>();

        /// <summary>拥有的职业 ClassData</summary>
        public List<string> ClassDataKeyList = new List<string>();
        // /// <summary>战技 SkillData</summary>
        public List<string> CombatArtKeyList = new List<string>();

        // /// <summary>魔法 SkillData</summary>
        public List<string> MagicKeyList = new List<string>();
        
        /// <summary>可以学会的技能 LearnSkillData</summary>
        public List<LearnSkillData> LearnSkillKeyList = new List<LearnSkillData>();
    }

    [Serializable]
    public class LearnSkillData
    {
        /// <summary>名字</summary>
        public string Name;

        /// <summary>需要角色等级</summary>
        public int NeedLv;

        /// <summary>需要技巧等级</summary>
        public TechniqueLevel TechnieuqLv;

        /// <summary>学会技能ID</summary>
        public string SkillName;
    }
}
