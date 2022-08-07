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
                    if (!XSUE.IsEditor())
                    {
                        Debug.LogError("XSEditorInstance.Instance: 只能在编辑器中使用");
                        return null;
                    }
                    
                    instance = new XSEditorInstance();
                    instance.GridHelper = Component.FindObjectOfType<XSGridHelper>();
                    instance.GridMgr = new GridMgr(instance.GridHelper);
                    instance.GridMgr.Init(instance.GridHelper);
                    instance.GridHelperEditMode = Component.FindObjectOfType<XSGridHelperEditMode>();
                }
                return instance;
            }
        }
        
        public IGridMgr GridMgr { get; set; } = null;
        public XSGridHelper GridHelper { get; set; } = null;
        public XSGridHelperEditMode GridHelperEditMode { get; set; } = null;

        /************************* 变量  end  ***********************/
    }
}