namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对象
    /// </summary>
    public class SkillFactory
    {
        /************************* 所有框架内的对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建SkillBase
        /// </summary>
        /// <param name="name">技能name</param>
        /// <param name="unit">玩家</param>
        /// <returns></returns>
        public static SkillBase CreateSkill(string name, UnitBase unit)
        {
            var data = TableManager.Instance.SkillDataManager.GetItem(name);
            SkillBase ret = CreateSkill(data, unit);
            return ret;
        }
        
        /// <summary>
        /// 工厂模式创建SkillBase
        /// </summary>
        /// <param name="name">技能name</param>
        /// <param name="unit">玩家</param>
        /// <returns></returns>
        public static SkillBase CreateSkill(SkillData data, UnitBase unit)
        {
            SkillBase ret;
            if (data == null)
            {
                ret = CreateSkillNull(unit);
            }
            else
            {
                switch (data.Type)
                {
                    case XSDefine.SkillType.Combat: ret = new SkillCombat(data, unit); break;
                    case XSDefine.SkillType.Magic: ret = new SkillMagic(data, unit); break;
                    case XSDefine.SkillType.Common:
                    default: ret = new SkillBase(data, unit); break;
                }
            }

            return ret;
        }

        /// <summary>
        /// 工厂模式创建SkillNull
        /// </summary>
        /// <param name="data">技能data</param>
        /// <param name="unit">触发器触发的对象</param>
        /// <returns></returns>
        public static SkillNull CreateSkillNull(UnitBase unit) => new SkillNull(new SkillData(), unit);

        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
