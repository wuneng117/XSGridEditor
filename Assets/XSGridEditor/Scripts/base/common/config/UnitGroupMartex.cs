using System.Collections.Generic;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/5
/// @Description: 一个矩阵表格，表示某个GroupTyped的谁可以攻击谁
/// </summary>
namespace XSSLG
{
    /// <summary>  </summary>
    public partial class Config
    {
        /// <summary> 一个矩阵表格，表示某个GroupTyped的谁可以攻击谁 </summary>
        public static List<List<bool>> UNIT_GROUP_MARTEX;

        /// <summary> 初始化矩阵表格 </summary>
        public static void InitUnitGroupMartex()
        {

            UNIT_GROUP_MARTEX = new List<List<bool>>(new List<bool>[4]);

            UNIT_GROUP_MARTEX[(int)GroupType.Self] = new List<bool>(new bool[4]);
            UNIT_GROUP_MARTEX[(int)GroupType.Self][(int)GroupType.Self] = false;
            UNIT_GROUP_MARTEX[(int)GroupType.Self][(int)GroupType.Enemy] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.Self][(int)GroupType.NpcFriend] = false;
            UNIT_GROUP_MARTEX[(int)GroupType.Self][(int)GroupType.Npc] = true;

            UNIT_GROUP_MARTEX[(int)GroupType.Enemy] = new List<bool>(new bool[4]);
            UNIT_GROUP_MARTEX[(int)GroupType.Enemy][(int)GroupType.Self] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.Enemy][(int)GroupType.Enemy] = false;
            UNIT_GROUP_MARTEX[(int)GroupType.Enemy][(int)GroupType.NpcFriend] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.Enemy][(int)GroupType.Npc] = true;

            UNIT_GROUP_MARTEX[(int)GroupType.NpcFriend] = new List<bool>(new bool[4]);
            UNIT_GROUP_MARTEX[(int)GroupType.NpcFriend][(int)GroupType.Self] = false;
            UNIT_GROUP_MARTEX[(int)GroupType.NpcFriend][(int)GroupType.Enemy] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.NpcFriend][(int)GroupType.NpcFriend] = false;
            UNIT_GROUP_MARTEX[(int)GroupType.NpcFriend][(int)GroupType.Npc] = true;

            UNIT_GROUP_MARTEX[(int)GroupType.Npc] = new List<bool>(new bool[4]);
            UNIT_GROUP_MARTEX[(int)GroupType.Npc][(int)GroupType.Self] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.Npc][(int)GroupType.Enemy] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.Npc][(int)GroupType.NpcFriend] = true;
            UNIT_GROUP_MARTEX[(int)GroupType.Npc][(int)GroupType.Npc] = false;
        }
    }
}