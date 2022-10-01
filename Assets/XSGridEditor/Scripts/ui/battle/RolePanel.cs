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


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary> 设置面板显示 </summary>

        public void init(UnitBase unit)
        {
            var role = unit.Role;
            this.nameText.text = role.Data.Name;
            this.classText.text = role.Class.Data.Name;
            this.LvText.text = role.Level.Lv.ToString();
            this.ExpText.text = role.Level.Exp.ToString();
            var stat = unit.GetStat();
            this.hpText.text = (stat.GetHP().GetFinal().ToString());
            this.movText.text = (stat.GetMov().GetFinal().ToString());
            this.strText.SetValue(stat.GetStr().GetFinal(), BattleDefine.MAX_ATTR);
            this.defText.SetValue(stat.GetDef().GetFinal(), BattleDefine.MAX_ATTR);
            this.magText.SetValue(stat.GetMag().GetFinal(), BattleDefine.MAX_ATTR);
            this.dexText.SetValue(stat.GetDex().GetFinal(), BattleDefine.MAX_ATTR);
            this.spdText.SetValue(stat.GetSpd().GetFinal(), BattleDefine.MAX_ATTR);
            this.resText.SetValue(stat.GetRes().GetFinal(), BattleDefine.MAX_ATTR);
            this.lckText.SetValue(stat.GetLck().GetFinal(), BattleDefine.MAX_ATTR);
            this.chaText.SetValue(stat.GetCha().GetFinal(), BattleDefine.MAX_ATTR);
        }
        
        /************************* 按钮回调 begin ***********************/

        public void OnClickClose()
        {
            BattleNode battleNode = XSUG.GetBattleNode();
            battleNode.CloseRolePanel();
        }
        /************************* 按钮回调  end  ***********************/
    }
}
