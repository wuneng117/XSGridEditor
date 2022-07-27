using UnityEngine;

namespace XSSLG
{
    /// <summary>  </summary>
    public class XSEditorInstance
    {
        /************************* 变量 begin ***********************/
        private static XSEditorInstance instance = null;
        public static XSEditorInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XSEditorInstance();
                    instance.GridHelper = Component.FindObjectOfType<XSGridHelper>();
                    instance.GridMgr = new GridMgr(instance.GridHelper);
                    instance.GridMgr.Init();
                    instance.GridHelperEditMode = Component.FindObjectOfType<XSGridHelperEditMode>();
                }
                return instance;
            }
        }
        
        public GridMgr GridMgr { get; set; } = null;
        public XSGridHelper GridHelper { get; set; } = null;
        public XSGridHelperEditMode GridHelperEditMode { get; set; } = null;

        /************************* 变量  end  ***********************/
    }
}