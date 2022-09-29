/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:33:23
/// @Description: script added to TileRoot gameobject
/// </summary>
using UnityEngine;

namespace XSSLG
{
    public class XSTileRootCpt : MonoBehaviour, XSITileRoot
    {
        public virtual Vector3 InverseTransformPoint(Vector3 Pos) => this.transform.InverseTransformPoint(Pos);
        public virtual Vector3 TransformPoint(Vector3 pos) => this.transform.TransformPoint(pos);
        public virtual void ClearAllTiles()=> XSUE.RemoveChildren(this.gameObject);
        public virtual bool IsNull() => this == null;
    }
}
