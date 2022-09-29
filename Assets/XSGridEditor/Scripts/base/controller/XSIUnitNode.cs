/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: interface to XSUnitNode
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSIUnitNode : XSINode
    {
        void AddBoxCollider();

        List<Vector3> GetMoveRegion();

        void UpdatePos();
    }
}