/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: 游戏架构和 unity 组件的接口
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSIUnitNode : XSINode
    {
        void AddBoxCollider();

        List<Vector3> GetMoveRegion();
    }
}