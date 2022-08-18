using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface ITileRoot
    {
        Vector3 InverseTransformPoint(Vector3 Pos);
        Vector3 TransformPoint(Vector3 pos);
        void ClearAllTiles();
    }
}