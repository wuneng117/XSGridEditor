using Vector3 = UnityEngine.Vector3;
namespace XSSLG
{
    public interface XSITileNode
    {
        void UpdateEditModePrevPos();
        void UpdateWorldPos(Vector3 worldPos);
        public void AddBoxCollider(Vector3 tileSize);


    }
}