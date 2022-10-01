using UnityEngine;

namespace XSSLG
{
    public class XSInstance
    {
        /************************* variable begin ***********************/
        public static XSIGridMgr GridMgr { get; set; }
        public static XSGridHelper GridHelper { get; set; }

        protected static XSGridHelperEditMode gridHelperEditMode;
        public static XSGridHelperEditMode GridHelperEditMode
        {
            get => gridHelperEditMode;
            set => gridHelperEditMode = value;
        }

        public static UIMgr UIMgr { get; set; } = new UIMgr();

        /************************* variable  end  ***********************/

        public static void Init()
        {
            GridHelper = Component.FindObjectOfType<XSGridHelper>();


            var tileRoot = GridHelper.TileRoot;
            Debug.Assert(tileRoot);

            var tileRootCpt = tileRoot.GetComponent<XSITileRoot>();
            Debug.Assert(tileRootCpt != null && !tileRootCpt.IsNull());

            var grid = tileRoot.GetComponent<Grid>();
            Debug.Assert(grid);
            GridMgr = new XSGridMgr(tileRootCpt, grid.cellSize);
            GridMgr.Init(GridHelper);

            GridHelperEditMode = Component.FindObjectOfType<XSGridHelperEditMode>();
        }
    }
}