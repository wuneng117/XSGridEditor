
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class PropStruct
    {
        /// <summary>填需要用到的StatData里列的名字</summary>
        [SerializeField]
        public string Type { get; set; }

        /// <summary>Type的百分比系数</summary>
        [SerializeField]
        public float Percent { get; set; }

        /// <summary>加上Type的百分比就是最终数值</summary>
        [SerializeField]
        public float Prop { get; set; }
    }

    [Serializable]
    public class SkillEffectStruct
    {
        /// <summary>效果类型</summary>
        [SerializeField]
        public SkillEffectType Type { get; set; }

        /// <summary>数值</summary>
        [SerializeField]
        public float Prop { get; set; }
    }

    [Serializable]
    public partial class TechniqueLevel
    {
        /// <summary>技能类型</summary>
        [SerializeField]
        protected XSDefine.TechniqueType type;
        public XSDefine.TechniqueType Type { get => type; set => type = value; }

        /// <summary>等级</summary>
        [SerializeField]
        protected int lv;
        public int Lv { get => lv; set => lv = value; }
    }

    public enum CompareType
    {
        None = 0,
        /// <summary>相等</summary>
        Equal = 1,
        /// <summary>小于</summary>
        LessThan = 2,
        /// <summary>大于</summary>
        MoreThan = 3,
        Max
    }


    public enum SkillEffectType
    {
        None = 0,
        /// <summary>物理免伤百分比</summary>
        PhyImmunityFactor = 1,
        /// <summary>物理伤害百分比</summary>
        PhyDamageFactor = 2,
        Max
    }


    public enum TargetType
    {
        Self = 0,
        Enemy = 0,
        Max
    }

    public enum WeaponType
    {
        None = 0,
        /// <summary>剑</summary>
        Sword = 1,
        /// <summary>枪</summary>
        Lance = 2,
        /// <summary>斧</summary>
        Axe = 3,
        /// <summary>弓</summary>
        Bow = 4,
        /// <summary>拳套</summary>
        Brawl = 5,
        Max
    }
}
