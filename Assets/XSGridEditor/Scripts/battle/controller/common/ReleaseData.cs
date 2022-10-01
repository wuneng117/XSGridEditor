using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 触发器触发数据基础类，主动技能判断是否可以使用时用到，因为只要传递src </summary>
    public class OnTriggerDataBase
    {
        public UnitBase Src { get; }

        /// <summary> 释放链，用来无效一些技能 </summary>
        public List<SkillBase> Chain { get; } = new List<SkillBase>();

        public OnTriggerDataBase(UnitBase src)
        {
            Debug.Assert(src != null);
            this.Src = src;
        }
    }

    /// <summary> 带坐标的触发数据，主动技能使用时用到，传递src和点击的坐标，来判断是否CanRelease </summary>
    public class OnTriggerDataCommon : OnTriggerDataBase
    {
        public XSTile Tile;
        public OnTriggerDataCommon(UnitBase src, XSTile tile) : base(src)
        {
            this.Tile = tile;
        }
    }

    /// <summary> 触发器触发数据最常用的结构 </summary>
    public class OnTriggerData : OnTriggerDataBase
    {
        public List<UnitBase> Dst { get; } = new List<UnitBase>();

        public OnTriggerData(UnitBase src, List<UnitBase> dst = null) : base(src)
        {
            // 如果没有目标那么dst肯定也是src
            if (dst == null || dst.Count == 0)
                this.Dst.Add(src);
            else
                this.Dst.AddRange(dst);
        }

        /// <summary>
        /// 多个参数传递
        /// </summary>
        /// <param name="src"></param
        /// <param name="dst">多个参数啊啊啊啊</param>
        public OnTriggerData(UnitBase src, params UnitBase[] dst) : this(src, new List<UnitBase>(dst)) { }
    }

    public class OnTriggerDataAttack : OnTriggerData
    {
        public List<int> SrcCauseDamage = new List<int>();

        /// <summary> 敌人对 </summary>
        public int DstCauseDamage = 0;

        public OnTriggerDataAttack(UnitBase src, List<UnitBase> dst, List<int> srcCauseDamage, int dstCauseDamage = 0) : base(src, dst)
        {
            (this.SrcCauseDamage, this.DstCauseDamage) = (srcCauseDamage, dstCauseDamage);
        }
    }

    /// <summary> trigger传递给skill的数据 </summary>
    public class ReleaseData
    {
        /// <summary> 触发时传递的ReleaseData </summary>
        public OnTriggerDataBase OnTriggerData { get; }

        /// <summary>skill要处理的对象，可能是releasedata.src或dst，也可能是trigger索敌出来的对象</summary>
        public List<UnitBase> Target { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="releaseData">触发时的数据</param>
        /// <param name="target">skill要处理的对象，可能是releasedata.src或dst，也可能是trigger索敌出来的对象</param>
        public ReleaseData(OnTriggerDataBase releaseData, List<UnitBase> target)
        {
            Debug.Assert(releaseData != null);
            Debug.Assert(target != null);
            (this.OnTriggerData, this.Target) = (releaseData, target);
        }
    }
}