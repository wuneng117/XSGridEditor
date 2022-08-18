using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSIUnitNode
    {
        void AddBoxCollider();

        List<Vector3> GetMoveRegion();

        Vector3 WorldPos { get; }

    }
}