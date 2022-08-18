using UnityEngine;

namespace XSSLG
{
    public class XSTileRootCpt : MonoBehaviour, XSITileRoot
    {
        public Vector3 InverseTransformPoint(Vector3 Pos) => this.transform.InverseTransformPoint(Pos);
        public Vector3 TransformPoint(Vector3 pos) => this.transform.TransformPoint(pos);
        public void ClearAllTiles()=> XSUE.RemoveChildren(this.gameObject);
    }
}
