using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using XSSLG;

[DefaultExecutionOrder(-1000)]
[ExecuteInEditMode]
public class XSMain : MonoBehaviour
{
    /// <summary> unit 管理 </summary>
    public XSUnitMgr UnitMgr { get; protected set; }
    /// <summary> XSPrefabBrush 画的prefab管理 </summary>
    public XSPrefabNodeMgr PrefabNodeMgr { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        // 最先初始化单例，保证使用时肯定是初始化好的
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
