﻿using System;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class BaseData
    {
        /// <summary> key名字 </summary>
        [SerializeField]
        [HideInInspector]
        protected string key = "";
        public string Key { get => key; set => key = value; }

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