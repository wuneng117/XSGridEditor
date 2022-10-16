/// <summary>
/// @Author: zhoutao
/// @Date: 2021/11/29
/// @Description: 玩家状态面板
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;

using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Linq;
using System;

namespace XSSLG
{
    public class RolePanel : MonoBehaviour
    {
        public Text nameText;
        public Text classText;
        public Text LvText;
        public Text ExpText;
        public Text hpText;
        public Text movText;

        public RolePanelaAttr strText;
        public RolePanelaAttr defText;
        public RolePanelaAttr magText;
        public RolePanelaAttr dexText;
        public RolePanelaAttr spdText;
        public RolePanelaAttr resText;
        public RolePanelaAttr lckText;
        public RolePanelaAttr chaText;

        /// <summary> 设置面板显示 </summary>

        public void init(UnitBase unit)
        {
            var role = unit.Role;
            this.nameText.text = role.Data.Name;
            this.classText.text = role.Class.Data.Name;
            this.LvText.text = role.Level.Lv.ToString();
            this.ExpText.text = role.Level.Exp.ToString();
            var stat = unit.GetStat();
            this.hpText.text = (stat.HP.GetFinal().ToString());
            this.movText.text = (stat.Mov.GetFinal().ToString());
            this.strText.SetValue(stat.Str.GetFinal(), XSDefine.MAX_ATTR);
            this.defText.SetValue(stat.Def.GetFinal(), XSDefine.MAX_ATTR);
            this.magText.SetValue(stat.Mag.GetFinal(), XSDefine.MAX_ATTR);
            this.dexText.SetValue(stat.Dex.GetFinal(), XSDefine.MAX_ATTR);
            this.spdText.SetValue(stat.Spd.GetFinal(), XSDefine.MAX_ATTR);
            this.resText.SetValue(stat.Res.GetFinal(), XSDefine.MAX_ATTR);
            this.lckText.SetValue(stat.Lck.GetFinal(), XSDefine.MAX_ATTR);
            this.chaText.SetValue(stat.Cha.GetFinal(), XSDefine.MAX_ATTR);
        }
        
        /************************* 按钮回调 begin ***********************/

        public void OnClickClose()
        {
            BattleNode battleNode = XSU.GetBattleNode();
            battleNode.CloseRolePanel();
        }
        /************************* 按钮回调  end  ***********************/
    }
}
