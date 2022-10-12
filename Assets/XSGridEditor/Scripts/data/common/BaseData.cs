using System;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class BaseData
    {
        /// <summary>名字</summary>
        [SerializeField]
        protected string name = "";
        public string Name { get => name; set => name = value; }

        /// <summary>描述</summary>
        [SerializeField]
        protected string desc = "";
        public string Desc { get => desc; set => desc = value; }
    }
}