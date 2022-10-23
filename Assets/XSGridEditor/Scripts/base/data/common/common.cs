
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
        protected TechniqueType type;
        public TechniqueType Type { get => type; set => type = value; }

        /// <summary>等级</summary>
        [SerializeField]
        protected int lv;
        public int Lv { get => lv; set => lv = value; }
    }
}
