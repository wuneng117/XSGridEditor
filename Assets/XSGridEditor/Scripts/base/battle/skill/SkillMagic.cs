namespace XSSLG
{
    /// <summary> 主动技能，魔法 </summary>
    public class SkillMagic : SkillBase
    {
        /************************* 变量 begin ***********************/
        protected TriggerManual trigger;
        public override TriggerBase Trigger => trigger;


        /************************* 变量  end  ***********************/
        public SkillMagic(SkillData data, UnitBase unit) : base(data, unit)
        {
            this.trigger = TriggerFactory.CreateTriggerManual(data.TriggerData, this);
        }
    }
}