using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;

[DefaultExecutionOrder(-1000)]
[ExecuteInEditMode]
public class XSMain : MonoBehaviour
{
    /// <summary> unit 管理 </summary>
    public XSUnitMgr UnitMgr { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        // 最先初始化单例，保证使用时肯定是初始化好的
        if (XSUnityUtils.IsEditor())
        {
            var instance = XSInstance.Instance;
            this.UnitMgr = new XSUnitMgr(instance.GridHelper);
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
