
using System;

namespace XSSLG
{
    [Serializable]
    public class PropStruct
    {
        /// <summary>填需要用到的StatData里列的名字</summary>
        public string Type;

        /// <summary>Type的百分比系数</summary>
        public float Percent;

        /// <summary>加上Type的百分比就是最终数值</summary>
        public float Prop;
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
