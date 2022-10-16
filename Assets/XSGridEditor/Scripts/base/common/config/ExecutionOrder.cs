/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 脚本自定义执行顺序，主要管理Awake函数的顺序
/// </summary>
namespace XSSLG
{
    public partial class Config
    {
        /// <summary> 自定义脚本执行顺序 </summary>
        public class ExecutionOrder
        {
            public const int MAIN = -1000;  // 入口函数，永远最小
            public const int MAINEDITMODE = -999;  // 入口函数的编辑器模式脚本
            public const int BATTLEBOX_LOADER = -350;  // battleboxmanager的初始化，先读取地图信息，再战斗初始化
            public const int BATTLE_INIT = -300;  // 战斗初始化，也挺小的
        }
    }
}