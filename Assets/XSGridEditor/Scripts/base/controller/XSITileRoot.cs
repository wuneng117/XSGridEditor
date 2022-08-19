/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: 游戏架构和 unity 组件的接口
/// </summary>
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSITileRoot
    {
        Vector3 InverseTransformPoint(Vector3 Pos);
        Vector3 TransformPoint(Vector3 pos);
        void ClearAllTiles();
    }
}