namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对象
    /// </summary>
    public class BuffFactory
    {
        /************************* 所有框架内的对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建BuffBase
        /// </summary>
        /// <param name="data">技能data</param>
        /// <param name="unit">玩家</param>
        /// <returns></returns>
        public static BuffBase CreateBuff(BuffData data, SkillBase skill)
        {
            var ret = new BuffBase(data, skill);
            return ret;
        }

        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
