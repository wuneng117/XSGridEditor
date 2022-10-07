
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    public partial class CommonTypeHolder
    {
        public global::XSSLG.CompareType Hold1 { get; private set; }

        public global::XSSLG.PropStruct Hold2 { get; private set; }

        public global::XSSLG.SkillEffectStruct Hold3 { get; private set; }

        public global::XSSLG.SkillEffectType Hold4 { get; private set; }

        public global::XSSLG.TargetType Hold5 { get; private set; }

        public global::XSSLG.TechniqueLevel Hold6 { get; private set; }

        public XSDefine.TechniqueType Hold7 { get; private set; }

        public global::XSSLG.WeaponType Hold8 { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class PropStruct
    {
        /// <summary>填需要用到的StatData里列的名字</summary>
        [System.ComponentModel.DisplayName("填需要用到的StatData里列的名字")]
        public string Type { get; private set; }

        /// <summary>Type的百分比系数</summary>
        [System.ComponentModel.DisplayName("Type的百分比系数")]
        public float Percent { get; private set; }

        /// <summary>加上Type的百分比就是最终数值</summary>
        [System.ComponentModel.DisplayName("加上Type的百分比就是最终数值")]
        public float Prop { get; private set; }

        /// <summary>备注</summary>
        [System.ComponentModel.DisplayName("备注")]
        public string Desc { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class SkillEffectStruct
    {
        /// <summary>效果类型</summary>
        [System.ComponentModel.DisplayName("效果类型")]
        public global::XSSLG.SkillEffectType Type { get; private set; }

        /// <summary>数值</summary>
        [System.ComponentModel.DisplayName("数值")]
        public float Prop { get; private set; }

        /// <summary>说明</summary>
        [System.ComponentModel.DisplayName("说明")]
        public string Desc { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    [Serializable]
    public partial class TechniqueLevel
    {
        /// <summary>技能类型</summary>
        [SerializeField]
        protected XSDefine.TechniqueType type;
        [System.ComponentModel.DisplayName("技能类型")]
        public XSDefine.TechniqueType Type { get => type; set => type = value; }

        /// <summary>等级</summary>
        [SerializeField]
        protected int lv;
        [System.ComponentModel.DisplayName("等级")]
        public int Lv { get => lv; set => lv = value; }
    }

    public enum CompareType
    {
        None = 0,
        /// <summary>相等</summary>
        [System.ComponentModel.Description("相等")]
        Equal = 1,
        /// <summary>小于</summary>
        [System.ComponentModel.Description("小于")]
        LessThan = 2,
        /// <summary>大于</summary>
        [System.ComponentModel.Description("大于")]
        MoreThan = 3,
    }


    public enum SkillEffectType
    {
        None = 0,
        /// <summary>物理免伤百分比</summary>
        [System.ComponentModel.Description("物理免伤百分比")]
        PhyImmunityFactor = 1,
        /// <summary>物理伤害百分比</summary>
        [System.ComponentModel.Description("物理伤害百分比")]
        PhyDamageFactor = 2,
    }


    public enum TargetType
    {
        Self = 0,
        Enemy = 0,
    }

    public enum WeaponType
    {
        None = 0,
        /// <summary>剑</summary>
        [System.ComponentModel.Description("剑")]
        Sword = 1,
        /// <summary>枪</summary>
        [System.ComponentModel.Description("枪")]
        Lance = 2,
        /// <summary>斧</summary>
        [System.ComponentModel.Description("斧")]
        Axe = 3,
        /// <summary>弓</summary>
        [System.ComponentModel.Description("弓")]
        Bow = 4,
        /// <summary>拳套</summary>
        [System.ComponentModel.Description("拳套")]
        Brawl = 5,
    }


}
