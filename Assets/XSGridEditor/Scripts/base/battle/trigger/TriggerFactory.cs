namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对象
    /// </summary>
    public class TriggerFactory
    {
        /************************* 所有框架内的对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建TriggerBase
        /// </summary>
        /// <param name="data">触发器name</param>
        /// <param name="releaseEntity">触发器触发的对象</param>
        /// <returns></returns>
        public static TriggerPassive CreateTriggerPassive(TriggerData data, IReleaseEntity releaseEntity)
        {
            if (data == null)
            {
                data = new TriggerData();
            }

            // else if (data.Type == TriggerType.ClickCombat || data.Type == TriggerType.ClickMagic)
            // {
                // return new TriggerManual(data, releaseEntity);
            // }
            // else
            // {
                return new TriggerPassive(data, releaseEntity);
            // }
        }

        /// <summary>
        /// 工厂模式创建TriggerNull
        /// </summary>
        /// <param name="releaseEntity">触发器触发的对象</param>
        /// <returns></returns>
        public static TriggerNull CreateTriggerNull(IReleaseEntity releaseEntity) => new TriggerNull(new TriggerData(), releaseEntity);

        
        /// <summary>
        /// 工厂模式创建TriggerManual
        /// </summary>
        /// <param name="data">触发器name</param>
        /// <param name="releaseEntity">触发器触发的对象</param>
        /// <returns></returns>
        public static TriggerManual CreateTriggerManual(TriggerData data, IReleaseEntity releaseEntity) => new TriggerManual(data, releaseEntity);
        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
