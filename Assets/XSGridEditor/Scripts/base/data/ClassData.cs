using System;
using System.Collections.Generic;

namespace XSSLG
{
    [Serializable]
    public class ClassData : BaseData
    {
        /// <summary>职业的级别类型</summary>
        public ClassLvType LvType;

        /// <summary>需要技巧等级</summary>
        public List<TechniqueLevel> TechniqueLvList = new List<TechniqueLevel>();

        /// <summary>职业附加属性</summary>
        public Stat Stat = new Stat();

        /// <summary>可以用魔法吗</summary>
        public bool CanMag;
    }
}
