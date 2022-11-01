namespace XSSLG
{
    /// <summary> 被动技能 </summary>
    public class SkillPassive : SkillBase
    {
        /************************* 变量 begin ***********************/
        public override Stat Stat
        {
            get
            {
                // TODO 各种条件检查
                return this.Data.Stat;

            }
        }

        /************************* 变量  end  ***********************/
        public SkillPassive(SkillData data, UnitBase unit) : base(data, unit)
        {

        }
    }
}