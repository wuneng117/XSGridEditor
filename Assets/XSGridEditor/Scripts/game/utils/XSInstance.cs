using UnityEngine;

namespace XSSLG
{
    /// <summary>  </summary>
    public class XSInstance
    {
        /************************* 变量 begin ***********************/
        private static XSInstance instance = null;
        public static XSInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XSInstance();
                    instance.GridHelper = Component.FindObjectOfType<XSGridHelper>();


                    var tileRoot = instance.GridHelper.TileRoot;
                    Debug.Assert(tileRoot);

                    var tileRootCpt = tileRoot.GetComponent<XSITileRoot>();
                    Debug.Assert(tileRootCpt != null);
                    
                     var grid = tileRoot.GetComponent<Grid>();
                    Debug.Assert(grid);
                    instance.GridMgr = new XSGridMgr(tileRootCpt, grid.cellSize);
                    instance.GridMgr.Init(instance.GridHelper);
                }
                return instance;
            }
        }
        public XSIGridMgr GridMgr { get; set; } = null;
        public XSGridHelper GridHelper { get; set; } = null;

        /************************* 变量  end  ***********************/
    }
}