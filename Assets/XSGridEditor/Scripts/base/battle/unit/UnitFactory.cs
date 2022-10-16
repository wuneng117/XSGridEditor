/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 用单例实现的抽象工厂模式去创建对象
/// </summary>
using System;
using UnityEngine;

namespace XSSLG
{
    /// <summary>
    /// 用单例实现的抽象工厂模式去创建对角色对象
    /// </summary>
    public class UnitFactory
    {
        /************************* 所有框架内的对象都是由工厂模式创建的 begin ***********************/
        /// <summary>
        /// 工厂模式创建
        /// </summary>
        /// <param name="data">roledata</param>
        public static Unit CreateUnit(RoleData data, GroupType group, XSIUnitNode unitNode)
        {
            if (data == null)
                return null;

            // var role = RoleFactory.CreateRole<Role>(TableManager.Instance.RoleDataManager.GetItem(XSSLG.StringToLong("Role0001")));
            var role = RoleFactory.CreateRole(data);
            if (role == null)
                return null;

            var unit = new Unit(role, group, unitNode);
            return unit;
        }

        /// <summary>
        /// 工厂模式创建敌人
        /// </summary>
        /// <param name="data">roledata</param>
        public static Unit CreateUnitEnemy(RoleData data, XSIUnitNode unitNode)
        {
            var ret = CreateUnit(data, GroupType.Enemy, unitNode);
            return ret;
        }

        /// <summary>
        /// 工厂模式创建我方
        /// </summary>
        /// <param name="data">roledata</param>
        public static Unit CreateUnit(RoleData data, XSIUnitNode unitNode)
        {
            var ret = CreateUnit(data, GroupType.Self, unitNode);
            return ret;
        }
        /************************* 所有框架内的对象都是由工厂模式创建的  end  ***********************/
    }
}
