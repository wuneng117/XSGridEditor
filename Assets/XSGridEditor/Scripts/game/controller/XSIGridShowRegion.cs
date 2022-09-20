/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: interface to XSIGridShowRegion
/// </summary>
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSIGridShowRegion
    {
        /// <summary>
        /// show range
        /// </summary>
        /// <param name="worldPosList"> a list of world position to show range </param>
        public void ShowRegion(List<Vector3> worldPosList);

        /// <summary> clear range </summary>
        public void ClearRegion();

        /// <summary> 
        /// You cannot  use "==" to check gameobject is null, because the gameobject in Unity is override "==", 
        /// but when the gameobject is converted to the XSIGridShowRegion, it will not use the override "==" 
        /// </summary>
        bool IsNull();
    }
}