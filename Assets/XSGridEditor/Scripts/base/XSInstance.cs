using UnityEngine;

namespace XSSLG
{
    /// <summary>  </summary>
    public class XSInstance
    {
        /************************* 变量 begin ***********************/
        private static XSInstance instance;
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
                    Debug.Assert(tileRootCpt != null && !tileRootCpt.IsNull());

                    var grid = tileRoot.GetComponent<Grid>();
                    Debug.Assert(grid);
                    instance.GridMgr = new XSGridMgr(tileRootCpt, grid.cellSize);
                    instance.GridMgr.Init(instance.GridHelper);


                    instance.GridHelperEditMode = Component.FindObjectOfType<XSGridHelperEditMode>();
                }
                return instance;
            }
        }
        public static void DestroyInstance() => instance = null;

        public XSIGridMgr GridMgr { get; set; }
        public XSGridHelper GridHelper { get; set; }

        protected XSGridHelperEditMode gridHelperEditMode;
        public XSGridHelperEditMode GridHelperEditMode 
        { 
            get
            {
                if (!XSUE.IsEditor())
                {
                    Debug.LogError("XSEditorInstance.Instance: 只能在编辑器中使用");
                    return null;
                }

                return this.gridHelperEditMode;
            }
            set => this.gridHelperEditMode = value; 
        }

        /************************* 变量  end  ***********************/
    }
}