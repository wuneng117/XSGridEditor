/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: 游戏架构和 unity 组件的接口
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

        /// <summary> 不能直接用==取判断null，因为unity里的Object重载了==， 但是转成XSIBrushItem类取判断的时候是不会用重载了的==</summary>
        bool IsNull();
    }
}