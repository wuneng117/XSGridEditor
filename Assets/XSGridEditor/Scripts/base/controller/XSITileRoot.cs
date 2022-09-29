/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: interface to XSTileRoot
/// </summary>
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSITileRoot
    {
        Vector3 InverseTransformPoint(Vector3 Pos);
        Vector3 TransformPoint(Vector3 pos);
        void ClearAllTiles();

        /// <summary> 
        /// You cannot  use "==" to check gameobject is null, because the gameobject in Unity is override "==", 
        /// but when the gameobject is converted to the XSITileRoot, it will not use the override "==" 
        /// </summary>
         bool IsNull();
    }
}