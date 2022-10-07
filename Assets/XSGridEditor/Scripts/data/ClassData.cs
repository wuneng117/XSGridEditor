using System;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class ClassData : BaseData
    {
        /// <summary>职业的级别类型</summary>
        [SerializeField]
        protected XSDefine.ClassLvType lvType;
        [System.ComponentModel.DisplayName("职业的级别类型")]
        public XSDefine.ClassLvType LvType { get => lvType; set => lvType = value; }

        /// <summary>需要技巧等级</summary>
        [SerializeField]
        [System.ComponentModel.DisplayName("需要技巧等级")]
        public List<TechniqueLevel> TechniqueLvArray { get; private set; } = new List<TechniqueLevel>();

        /// <summary>职业附加属性</summary>
        [SerializeField]
        protected Stat stat = new Stat();
        [System.ComponentModel.DisplayName("职业附加属性")]
        public Stat Stat { get => stat; set => stat = value; }

        /// <summary>可以用魔法吗</summary>
        [SerializeField]
        protected bool canMag;
        [System.ComponentModel.DisplayName("可以用魔法吗")]
        public bool CanMag { get => canMag; set => canMag = value; }
    }
}
