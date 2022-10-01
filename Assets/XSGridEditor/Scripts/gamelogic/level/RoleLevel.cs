/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 继承LevelBase，表示角色的等级
/// </summary>
using System;
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary> 角色等级 </summary>
    public class RoleLevel : LevelBase
    {
        public RoleLevel(int lv = 0, int exp = 0) : base(Config.ROLE_EXP_ARRAY, lv, exp) { }
    }
}