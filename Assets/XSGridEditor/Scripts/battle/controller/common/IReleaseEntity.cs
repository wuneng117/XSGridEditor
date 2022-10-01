namespace XSSLG
{
    /// <summary>
    /// TriggerBase对Skillbase对象的引用，因为解耦，所以只定义需要的接口
    /// </summary>
    public interface IReleaseEntity
    {
        /// <summary>
        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool CanRelease(ReleaseData data);

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="data"></param>
        bool Release(ReleaseData data);

        /// <summary> 保留的玩家对象 </summary>
        UnitBase Unit { get; }
    }
}
