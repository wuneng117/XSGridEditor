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
        /// <param name="name">触发器name</param>
        /// <param name="releaseEntity">触发器触发的对象</param>
        /// <returns></returns>
        public static TriggerBase CreateTrigger(string name, IReleaseEntity releaseEntity)
        {
            var data = TableManager.Instance.TriggerDataManager.GetItem(name);
            if (data == null)
            {
                return CreateTriggerNull(releaseEntity);
            }
            else if (data.Type == TriggerType.ClickCombat || data.Type == TriggerType.ClickMagic)
            {
                return new TriggerBase(data, releaseEntity);
            }
            else
            {
                return new AutoTriggerBase(data, releaseEntity);
            }
        }

        /// <summary>
        /// 工厂模式创建TriggerNull
        /// </summary>
        /// <param name="name">触发器触发的对象</param>
        /// <returns></returns>
        public static TriggerNull CreateTriggerNull(IReleaseEntity releaseEntity) => new TriggerNull(new TriggerData(), releaseEntity);
        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
