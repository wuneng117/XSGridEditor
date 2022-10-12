/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/8
/// @Description: 工厂模式
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对象
    /// </summary>
    public class SearchTargetFactory
    {
        /************************* 所有框架内的对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建SearchTarget
        /// </summary>
        /// <param name="data">触发器data</param>
        /// <param name="releaseEntity">触发器触发的对象</param>
        /// <returns></returns>
        public static SearchTargetBase CreateSearchTarget(SearchStruct searchStruct)
        {
            if (searchStruct == null)
                return new SearchTargetNull();

            switch (searchStruct.Type)
            {
                case SearchType.Front1: return new SearchTargetFront(searchStruct, 1);
                case SearchType.Front1x2: return new SearchTargetFront(searchStruct, 2);
                case SearchType.Front1x3: return new SearchTargetFront(searchStruct, 3);
                case SearchType.Front3x1: return new SearchTargetFrontWide(searchStruct, 0);
                case SearchType.Scope1: return new SearchTargetScope(searchStruct, 0);
                case SearchType.ScopeCross1: return new SearchTargetScope(searchStruct, 1);
                case SearchType.ScopeCross2: return new SearchTargetScope(searchStruct, 2);
                case SearchType.ScopeCross3: return new SearchTargetScope(searchStruct, 3);
                default: return new SearchTargetNull();
            }
        }
        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
