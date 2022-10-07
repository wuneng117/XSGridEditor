﻿
using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class StatData
    {
        /// <summary>ID</summary>
        [System.ComponentModel.DisplayName("ID")]
        public long Id { get; private set; }

        /// <summary>血量</summary>
        [System.ComponentModel.DisplayName("血量")]
        public int Hp { get; private set; }

        /// <summary>力量</summary>
        [System.ComponentModel.DisplayName("力量")]
        public int Str { get; private set; }

        /// <summary>魔力</summary>
        [System.ComponentModel.DisplayName("魔力")]
        public int Mag { get; private set; }

        /// <summary>技巧</summary>
        [System.ComponentModel.DisplayName("技巧")]
        public int Res { get; private set; }

        /// <summary>速度</summary>
        [System.ComponentModel.DisplayName("速度")]
        public int Spd { get; private set; }

        /// <summary>幸运</summary>
        [System.ComponentModel.DisplayName("幸运")]
        public int Lck { get; private set; }

        /// <summary>防御</summary>
        [System.ComponentModel.DisplayName("防御")]
        public int Def { get; private set; }

        /// <summary>魔防</summary>
        [System.ComponentModel.DisplayName("魔防")]
        public int Dex { get; private set; }

        /// <summary>魅力</summary>
        [System.ComponentModel.DisplayName("魅力")]
        public int Cha { get; private set; }

        /// <summary>移动</summary>
        [System.ComponentModel.DisplayName("移动")]
        public int Mov { get; private set; }

        /// <summary>血量百分比</summary>
        [System.ComponentModel.DisplayName("血量百分比")]
        public float HpFactor { get; private set; }

        /// <summary>力量百分比</summary>
        [System.ComponentModel.DisplayName("力量百分比")]
        public float StrFactor { get; private set; }

        /// <summary>魔力百分比</summary>
        [System.ComponentModel.DisplayName("魔力百分比")]
        public float MagFactor { get; private set; }

        /// <summary>技巧百分比</summary>
        [System.ComponentModel.DisplayName("技巧百分比")]
        public float ResFactor { get; private set; }

        /// <summary>速度百分比</summary>
        [System.ComponentModel.DisplayName("速度百分比")]
        public float SpdFactor { get; private set; }

        /// <summary>幸运百分比</summary>
        [System.ComponentModel.DisplayName("幸运百分比")]
        public float LckFactor { get; private set; }

        /// <summary>防御百分比</summary>
        [System.ComponentModel.DisplayName("防御百分比")]
        public float DefFactor { get; private set; }

        /// <summary>魔防百分比</summary>
        [System.ComponentModel.DisplayName("魔防百分比")]
        public float DexFactor { get; private set; }

        /// <summary>移动百分比</summary>
        [System.ComponentModel.DisplayName("移动百分比")]
        public float MovFactor { get; private set; }

        /// <summary>魅力百分比</summary>
        [System.ComponentModel.DisplayName("魅力百分比")]
        public float ChaFactor { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class StatDataArray
    {
        public List<long> Keys { get; private set; }

        public List<global::XSSLG.StatData> Items { get; private set; }

        public string TableHash { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

}
