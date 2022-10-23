using System;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class BaseData : XSIListViewData
    {
        /// <summary> key名字 </summary>
        [HideInInspector]
        public string Key = "";

        /// <summary>名字</summary>
        [SerializeField]
        protected string name = "";
        public string Name { get => name; set => name = value; }

        /// <summary>描述</summary>
        public string Desc = "";

        [SerializeField]
        protected Texture2D texture;
        public Texture2D Texture { get => texture; set => texture = value; }
    }
}