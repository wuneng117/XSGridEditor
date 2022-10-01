using UnityEngine;
using XSSLG;

[DefaultExecutionOrder(Config.ExecutionOrder.MAIN)]
[ExecuteInEditMode]
public class XSMainEditMode : MonoBehaviour
{
    public XSUnitMgrEditMode UnitMgrEditMode { get; protected set; }
    
    /// <summary> Prefab management of XSPrefabBrush painting </summary>
    public XSPrefabNodeMgr PrefabNodeMgr { get; protected set; }

    void Awake()
    {
        // Initialize the Instance first to ensure that it must be initialized when used
        if (XSU.IsEditor())
        {
            XSInstance.Init();
            this.PrefabNodeMgr = new XSPrefabNodeMgr();
            this.UnitMgrEditMode = new XSUnitMgrEditMode(XSInstance.GridHelper);
        }
    }
}
