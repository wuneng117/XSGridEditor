/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 
/// Attr的接口
/// </summary>
namespace XSSLG {
    public interface IAttr {
        /// <summary> 基础属性 </summary>
        int GetBase();

        /// <summary> 加成百分比 </summary>
        float GetFactor();

        /// <summary> 基础乘以百分比计算最终属性 </summary>
        int GetFinal();

        /// <summary> 加其它属性 </summary>
        void Add(IAttr attr);
    }
}