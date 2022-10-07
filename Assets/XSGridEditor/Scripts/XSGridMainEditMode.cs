using UnityEngine;
namespace XSSLG
{
    [DefaultExecutionOrder(Config.ExecutionOrder.MAINEDITMODE)]
    [ExecuteInEditMode]
    public class XSGridMainEditMode : MonoBehaviour
    {
        public XSUnitMgrEditMode UnitMgrEditMode { get; protected set; }

        /// <summary> Prefab management of XSPrefabBrush painting </summary>
        public XSPrefabNodeMgr PrefabNodeMgr { get; protected set; }

        protected XSGridHelperEditMode gridHelperEditMode;
        public XSGridHelperEditMode GridHelperEditMode
        {
            get => gridHelperEditMode;
            set => gridHelperEditMode = value;
        }

        public virtual void Awake()
        {
            // Initialize the Instance first to ensure that it must be initialized when used
            if (!XSU.IsEditor())
            {
                return;
            }

            this.PrefabNodeMgr = new XSPrefabNodeMgr();
            this.UnitMgrEditMode = new XSUnitMgrEditMode(XSU.GridHelper);
            this.GridHelperEditMode = Component.FindObjectOfType<XSGridHelperEditMode>();
        }
    }
}
