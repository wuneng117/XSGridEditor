/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/19
/// @Description: 技能和buff统一管理的地方
/// </summary>
/// 
using System;
using System.Collections.Generic;
/// <summary>
/// 技能和buff统一管理的地方
/// </summary>
namespace XSSLG
{
    public class NormalTable : CommonTable<ICommonTable<SkillUpdateData>, SkillUpdateData>
    {
        // 技能管理
        public SkillTable SkillTable { get; }
        // buff管理
        public BuffTable BuffTable { get; }

        public NormalTable()
        {
            this.SkillTable = new SkillTable();
            this.BuffTable = new BuffTable();
            this.List.Add(this.SkillTable);
            this.List.Add(this.BuffTable);
        }

        /// <summary>
        /// 初始化技能
        /// </summary>
        /// <param name="skillList"></param>
        public void InitSkill(List<SkillData> skillList, UnitBase unit) => this.SkillTable.Init(skillList, unit);

        public Stat GetStat()
        {
            var ret = new Stat();
            ret.Add(this.SkillTable.GetStat());
            ret.Add(this.BuffTable.GetStat());
            return ret;
        }
    }
}