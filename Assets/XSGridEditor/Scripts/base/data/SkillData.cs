
using System;
using System.Collections.Generic;

namespace XSSLG
{
    [Serializable]
    public partial class SkillData : BaseData
    {
        /// <summary>类型</summary>
        public SkillType Type = SkillType.Common;

        /// <summary>武器类型</summary>
        public WeaponType WeaponType;

        /// <summary> 伤害类型 </summary>
        public SKillDamageType DamageType;  

        /// <summary> 消耗武器耐久度 </summary>
        public int WeponCost;

        /// <summary> 不能暴击 </summary>
        public bool CannotCrit;

        /// <summary> 战技/魔法 基础威力 </summary>
        public int DamageBase;

        /// <summary> 战技/魔法根据属性增加威力 </summary>
        public List<StatSingle> StatSingleList = new List<StatSingle>();

        /// <summary>触发器id</summary>
        public string TriggerName;

        /// <summary>常用效果</summary>
        public List<SkillEffectStruct> EffectList = new List<SkillEffectStruct>();

        /// <summary>添加buffId数组</summary>
        public List<string> BuffKeyList = new List<string>();

        /// <summary> 技能存在时的一级属性加成, 类似被动技能 </summary>
        public Stat Stat = new Stat();

        /// <summary> 被动技能: 技能存在时的二级属性加成; 主动技能: 战斗时的二级属性加成 </summary>
        public SecondStat SecondStat = new SecondStat();
    }
}
