
using System;

namespace XSSLG
{
    [Serializable]
    public class StatSingle
    {
        /// <summary>填需要用到的StatData里属性的名字</summary>
        public string Type;

        /// <summary>属性的百分比系数</summary>
        public float Percent;
    }

    [Serializable]
    public class SkillEffectStruct
    {
        /// <summary>效果类型</summary>
        public SkillEffectType Type;

        /// <summary>数值</summary>
        public float Prop;
    }

    [Serializable]
    public partial class TechniqueLevel
    {
        /// <summary>技能类型</summary>
        public TechniqueType Type;

        /// <summary>等级</summary>
        public int Lv;
    }
}
