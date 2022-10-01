/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 
/// HP的接口
/// </summary>
namespace XSSLG {
    public interface IHP {


        /// <summary> 基础属性 </summary>
        int Get();

        /// <summary> 基础属性,和Get是一样的，只是为了和IAttr用起来一样 </summary>
        int GetBase();
        /// <summary> 基础属性,和Get是一样的，只是为了和IAttr用起来一样 </summary>
        int GetFinal();

        /// <summary> 加其它属性 </summary>
        void Add(IHP hp); 

        /// <summary> 加具体血量 </summary>
        void Add(int hp);

        /// <summary> 减具体血量 </summary>
        void Reduce(int hp);

        /// <summary> 获取最大血量 </summary>
        int GetMax();
        
        /// <summary> 获取最大血量属性 </summary>
        IAttr GetMaxAttr();

        /// <summary> 获取百分比：血量/最大血量 </summary>
        float GetPercent();
    }
}