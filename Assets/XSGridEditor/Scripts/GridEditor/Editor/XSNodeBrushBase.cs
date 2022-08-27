#if ENABLE_TILEMAP
using UnityEngine;

namespace XSSLG
{
    public abstract class XSNodeBrushBase<T> : XSBrushBase where T : class, XSIBrushItem
    {
        abstract protected XSBrushItemMgr<T> GetMgr();

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var mgr = this.GetMgr();
            if (mgr == null)
                return;

            var worldPos = gridLayout.CellToWorld(position);
            mgr.Remove(worldPos);
        }
    }
}

#endif