namespace XSSLG
{
    /// <summary> 战斗的常量定义 </summary>
    public partial class XSDefine
    {
        /************************* BattleScene节点路径 begin ***********************/
        public static readonly string SCENE_BATTLE_BOX_MANAGER_PATH = "GridMgrEditor";    // 3d地图编辑器脚本节点
        public static readonly string SCENE_UNITROOT_PATH = "component/UnitRoot"; // Unit的根节点

        public static readonly string SCENE_GRID_ROOT = "component/Grid"; // Grid根节点Froot?

        // grid网格贴花显示根节点
        public static readonly string SCENE_GRID_BASE = SCENE_GRID_ROOT + "/base"; // 所有网格显示的贴花
        public static readonly string SCENE_GRID_MOVE = SCENE_GRID_ROOT + "/move"; // unit移动范围网格显示的贴花
        public static readonly string SCENE_GRID_ATTACK_RANGE = SCENE_GRID_ROOT + "/AttackRange"; // unit攻击范围网格显示的贴花
        public static readonly string SCENE_GRID_ATTACK_EFFECT_RANGE = SCENE_GRID_ROOT + "/AttackEffectRange"; // unit攻击效果范围网格显示的贴花

        // unit在网格地图中配置显示根节点
        public static readonly string SCENE_GRID_UNIT_SPAWN_ENEMY = "component/GridUnitSpawnEditor/enemy"; // 敌人出生点显示根节点
        public static readonly string SCENE_GRID_UNIT_SPAWN_SELF = "component/GridUnitSpawnEditor/self"; // 自己强制出生点显示根节点
        public static readonly string SCENE_GRID_UNIT_SPAWN_SELF_ARENA = "component/GridUnitSpawnEditor/SelfArena"; // 自己出击点显示根节点


        /************************* BattleScene节点路径  end  ***********************/

        public static readonly int MAX_BUFF_DURATION = int.MaxValue;    // BUFF持续时间无限
        public static readonly int MAX_ATTR = 99;    // 属性最大值
        public static readonly string SKILL_ATTACK_ID = "Attack";    // 普通攻击在skilldata里的id

    }

    /// <summary> 地图上单位的朝向 </summary>
    public enum DirectionType
    {
        LeftTop = 0,
        LeftBottom = 1,
        RightTop = 2,
        RightBottom = 3
    }

    /// <summary> 势力 </summary>
    public enum GroupType
    {
        Self = 0,
        Enemy = 1,
        NpcFriend = 2,
        Npc = 3,
    }

    public enum UnitStatusType
    {
        Actived = 1 << 0,
        Dead = 1 << 1,
        Attacked = 1 << 2,
        Moved = 1 << 3,
    }
}