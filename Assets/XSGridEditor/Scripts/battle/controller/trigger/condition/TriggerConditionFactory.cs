namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对象
    /// </summary>
    public class TriggerConditionFactory
    {
        /************************* 所有框架内的对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建TriggerBase
        /// </summary>
        /// <param name="type">触发器触发类型</param>
        /// <param name="conditionStruct">触发器条件</param>
        /// <returns></returns>
        public static TriggerConditionBase Create(XSDefine.TriggerType type, ConditionStruct conditionStruct)
        {
            switch (type)
            {
                case XSDefine.TriggerType.AfterAttack: return new AfterAttackCondition(conditionStruct);
                default: return new TriggerConditionNull(conditionStruct);
            }
        }

        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
