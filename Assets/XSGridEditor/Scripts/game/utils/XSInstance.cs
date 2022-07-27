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
                    instance.GridMgr = new GridMgr(instance.GridHelper);
                    instance.GridMgr.Init();
                }
                return instance;
            }
        }
        public GridMgr GridMgr { get; set; } = null;
        public XSGridHelper GridHelper { get; set; } = null;

        /************************* 变量  end  ***********************/
    }
}