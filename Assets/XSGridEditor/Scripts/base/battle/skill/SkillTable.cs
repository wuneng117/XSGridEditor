using System;
using System.Collections.Generic;
using System.Linq;

namespace XSSLG
{
    /// <summary> 技能列表 </summary>
    public class SkillTable : CommonTable<SkillBase, SkillUpdateData>
    {
        /// <summary> 空技能，如果一个buff不是特定技能添加的，那它的skill指向这个变量 </summary>
        public SkillNull HeadSkill { get; private set; }
        public SkillBase AttackSkill { get; set; }
        /// <summary>
        /// 初始化技能
        /// </summary>
        /// <param name="skillList"></param>
        public void Init(List<SkillData> skillList, UnitBase unit)
        {
            this.HeadSkill = SkillFactory.CreateSkillNull(unit) ?? throw new System.ArgumentNullException("HeadSkill");
            this.List.Add(this.HeadSkill);
            this.AttackSkill = SkillFactory.CreateSkill(XSDefine.SKILL_ATTACK_ID, unit) ?? 
                throw new System.ArgumentNullException("HeadSkill");
            this.List.Add(this.AttackSkill);
            
            skillList.ForEach(skillData =>
            {
                // if (skillData.TriggerData == null)
                // {
                //     Debug.Assert(false, skillData.Name + ":麻烦填一下triggerid谢谢！");
                //     return;
                // }

                // if (skillData.Group == SkillDataSkillGroupType.Normal && skillData.TriggerData.Type != TriggerType.Common)
                // {
                //     Debug.Assert(false, skillData.Name + "是主动技能，但是触发器不是主动触发器，可能引起异常状态！");
                //     return;
                // }

                var skill = SkillFactory.CreateSkill(skillData, unit);
                this.List.Add(skill);
            });
        }

        /// <summary> 技能增加的属性 </summary>
        public Stat GetStat()
        {
            var ret = this.List.Aggregate(new Stat(), (ret, skill) => skill.Stat);
            return ret;
        }

        public List<SkillBase> GetCombatSkill() => this.List.FindAll(skill => skill.SkillType == SkillType.CombatArt);
    }
}