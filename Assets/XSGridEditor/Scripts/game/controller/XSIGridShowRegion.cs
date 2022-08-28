/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: 游戏架构和 unity 组件的接口
/// </summary>
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    /// <summary> 常用显示范围 </summary>
    public interface XSIGridShowRegion
    {
        /// <summary>
        /// 显示高亮范围
        /// </summary>
        /// <param name="worldPosList">图块所在的网格坐标</param>
        public void ShowRegion(List<Vector3> worldPosList);

        /// <summary> 清除高亮显示 </summary>
        public void ClearRegion();

        /// <summary> 不能直接用==取判断null，因为unity里的Object重载了==， 但是转成XSIBrushItem类取判断的时候是不会用重载了的==</summary>
        bool IsNull();
    }
}