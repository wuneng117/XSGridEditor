/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: interface to XSTileNode
/// </summary>
using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;

namespace XSSLG
{
    public interface XSITileNode
    {
        int Cost { get; }

        Accessibility Access { get; }

        Vector3 WorldPos { get; set; }

        int AngleY { get; }

        XSTile CreateXSTile(Vector3Int tilePos);

        void UpdateEditModePrevPos();

        void AddBoxCollider(Vector3 tileSize);

        void RemoveNode();

        /// <summary> 
        /// You cannot  use "==" to check gameobject is null, because the gameobject in Unity is override "==", 
        /// but when the gameobject is converted to the XSITileNode, it will not use the override "==" 
        /// </summary>
        bool IsNull();
    }
}