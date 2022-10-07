using UnityEngine;
namespace XSSLG
{
    [DefaultExecutionOrder(Config.ExecutionOrder.MAIN)]
    [ExecuteInEditMode]
    public class XSGridMain : MonoBehaviour
    {
        public XSIGridMgr GridMgr { get; protected set; }

        public XSGridHelper GridHelper { get; protected set; }

        public virtual void Awake()
        {
            this.GridHelper = Component.FindObjectOfType<XSGridHelper>();

            var tileRoot = GridHelper.TileRoot;
            Debug.Assert(tileRoot);

            var tileRootCpt = tileRoot.GetComponent<XSITileRoot>();
            Debug.Assert(tileRootCpt != null && !tileRootCpt.IsNull());

            var grid = tileRoot.GetComponent<Grid>();
            Debug.Assert(grid);
            this.GridMgr = new XSGridMgr(tileRootCpt, grid.cellSize);
            this.GridMgr.Init(this.GridHelper);
        }
    }
}