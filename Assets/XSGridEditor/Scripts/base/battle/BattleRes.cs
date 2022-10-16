namespace XSSLG
{
    /// <summary> 战斗中加载的动态资源 </summary>
    public class BattleRes
    {
        // grid网格贴花资源
        public static readonly string GRID_BASE = "editor/prefab/BaseGrid"; // 所有网格显示的贴花资源
        public static readonly string GRID_MOVE = "editor/prefab/MoveGrid"; // unit移动范围网格显示的贴花资源
        public static readonly string GRID_ATTACK_RANGE = "editor/prefab/AtkGrid"; // unit攻击范围网格显示的贴花资源
        public static readonly string GRID_ATTACK_EFFECT_RANGE = "editor/prefab/AtkEffectGrid"; // unit攻击效果范围网格显示的贴花资源
        
        // 网格地图中单位出生点编辑器中用于显示的prefab
        public static readonly string GRID_UNIT_SPAWN_ENEMY = "editor/prefab/EnemySpawn";    // 敌人出生点显示资源
        public static readonly string GRID_UNIT_SPAWN_SELF = "editor/prefab/SelfSpawn";    // 自己强制出生点显示资源
        public static readonly string GRID_UNIT_SPAWN_SELF_ARENA = "editor/prefab/SelfSpawnArena";    // 自己出击点显示资源
    }
}