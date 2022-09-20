using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using XSSLG;

[DefaultExecutionOrder(-1000)]
[ExecuteInEditMode]
public class XSMain : MonoBehaviour
{
    public XSUnitMgr UnitMgr { get; protected set; }
    /// <summary> Prefab management of XSPrefabBrush painting </summary>
    public XSPrefabNodeMgr PrefabNodeMgr { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Instance first to ensure that it must be initialized when used
        if (XSUnityUtils.IsEditor())
        {
            var instance = XSInstance.Instance;
            this.UnitMgr = new XSUnitMgr(instance.GridHelper);
            this.PrefabNodeMgr = new XSPrefabNodeMgr();
        }
        else
        {
            var instance = XSInstance.Instance;
        }
    }

    void OnDestroy()
    {
        XSInstance.DestroyInstance();
    }
}
