/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 用单例实现的抽象工厂模式去创建对象
/// </summary>
using System;


namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对角色对象
    /// </summary>
    public class RoleFactory
    {
        /************************* 复杂对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建
        /// </summary>
        /// <param name="data">roledata</param>
        /// <returns>role对象</returns>
        public static T CreateRole<T>(RoleData data)  where T : Role, new()
        {
            if (data == null)
                return null;

            var ret = new T();
            if (!ret.Init(data))
                return null;

            return ret;
        }

        /// <summary>
        /// 工厂模式创建
        /// </summary>
        /// <param name="data">roledata</param>
        /// <returns>role对象</returns>
        public static Role CreateRole(RoleData data) => RoleFactory.CreateRole<Role>(data);
        /************************* 复杂对象都是由工厂模式创建的  end  ***********************/
    }
}
