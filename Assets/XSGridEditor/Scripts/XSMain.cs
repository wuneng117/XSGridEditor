using UnityEngine;
using XSSLG;

[DefaultExecutionOrder(Config.ExecutionOrder.MAIN)]
public class XSMain : MonoBehaviour
{
    private static bool IsInit { get; set; }

    void Awake()
    {
        // var data = TableManager.Instance.RoleDataManager.GetItem(XSSLG.StringToLong("Role0001"));
        // 如果已经初始化过了，说明已经有一个持久节点了，不需要这个节点了
        if (IsInit)
        {
            Destroy(this);
            return;
        }

        Config.InitUnitGroupMartex();

        // Initialize the Instance first to ensure that it must be initialized when used
        XSInstance.Init();
        DontDestroyOnLoad(this.gameObject);
        // 设置为已经初始化，并且切换场景不释放
        IsInit = true;
    }
}
