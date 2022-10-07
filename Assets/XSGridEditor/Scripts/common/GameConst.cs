/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 游戏中的常量定义
/// </summary>
namespace XSSLG
{
    /// <summary> 游戏中的常量定义 </summary>
    public class GameConst
    {
        public static readonly bool IS_LOG = true;  // 是否打印输出
        public static readonly int TECHNIQUE_LEVEL_MAX = 11;    // 技巧最高等级
        public static readonly string[] TECHNIQUE_TXT_ARRAY = { "E", "E+", "D", "D+", "C", "C+", "B", "B+", "A", "A+", "S", "S+" };    // 技巧等级描述


        /************************* 节点名字 begin ***********************/
        public static readonly string COMPONENT_NAME_BATTLE_INIT = "BattleNode";    // BattleInit挂的节点名字
        public static readonly string COMPONENT_NAME_MAIN = "main"; // 全局唯一节点，换场景也不摧毁
        public static readonly string COMPONENT_NAME_GAMESCENE = "GameScene";   // GameScene的UI根节点上挂的组件
        public static readonly string COMPONENT_NAME_BATTLE_DEBUG = "BattleDebug";   // battle debug相关组件挂的节点名字
        
        /************************* 节点名字  end  ***********************/

        /************************* 路径 begin ***********************/
        public static readonly string BATTLEBOX_DATASET_PATH = "Assets/BuildSource/BattleboxDataset/";  // 战斗场景格子数据保存的路径
        
        /************************* 路径  end  ***********************/

        /************************* 图层 begin ***********************/
        public static readonly string LAYER_GROUND = "Ground";  // 地面层，战斗地图生成用
        /************************* 图层  end  ***********************/

        public static readonly string DATA_FILE_PATH_EDITOR = "Assets/XSGridEditor/Resources/data/";   // 编辑器中的数据文件路径
        public static readonly string DATA_FILE_PATH_RUNTIME = "data/";   // 运行时的数据文件路径
    }
}