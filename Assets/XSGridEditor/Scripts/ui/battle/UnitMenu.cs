/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/27
/// @Description: unit行动菜单
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
    public class UnitMenu : MonoBehaviour
    {
        /************************* 按钮 begin ***********************/
        public Button attackBtn;
        public Button combatBtn;

        /************************* 按钮  end  ***********************/

        /************************* 其他界面prefab begin ***********************/

        /// <summary> 选择用于攻击的武器界面 </summary>
        public MeanOfAttackPanel meanOfAttackPanel;
        /************************* 其他界面prefab  end  ***********************/

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnMouseUp()
        {

        }

        private void OnEnable()
        {
            var unit = XSUG.GetBattleLogic().UnitMgr.ActionUnit;
            if (unit == null)
                return;

            this.UpdateAttackBtn(unit);
            //TODO
            // this.UpdateCombatBtn(unit);
        }

        /// <summary> 显示攻击按钮 </summary>
        private void UpdateAttackBtn(Unit unit)
        {
            this.attackBtn.gameObject.SetActive(!unit.IsAttacked() && unit.Table.SkillTable.AttackSkill.Trigger.CanRelease(new OnTriggerDataBase(unit)));
        }

        /// <summary> 显示战技按钮 </summary>
        private void UpdateCombatBtn(Unit unit)
        {
            this.combatBtn.gameObject.SetActive(!unit.IsAttacked() && unit.Table.SkillTable.GetCombatSkill().Any(skill => skill.Trigger.CanRelease(new OnTriggerDataBase(unit))));
        }

        /************************* 按钮回调 begin ***********************/

        /// <summary> 点击攻击 </summary>
        public void OnClickAttack()
        {
            var unit = XSUG.GetBattleLogic().UnitMgr.ActionUnit;
            if (unit == null)
                return;

            var skill = unit.Table.SkillTable.AttackSkill;

            XSUG.UIMgr.ShowUI(this.meanOfAttackPanel?.gameObject);
            this.meanOfAttackPanel.transform.position = XSU.WorldPosToScreenPos(unit.WorldPos);
            this.meanOfAttackPanel.Init(skill);
        }

        /// <summary> 点击战技 </summary>
        public void OnClickCombatArt()
        {
            var unit = XSUG.GetBattleLogic().UnitMgr.ActionUnit;
            if (unit == null)
                return;

            var skillList = unit.Table.SkillTable.GetCombatSkill();
            var data = new OnTriggerDataBase(unit);
            skillList = skillList.FindAll(skill => skill.Trigger.CanRelease(data));
            //TODO 战技需要选择武器

        }

        
        /// <summary> 点击待机 </summary>
        public void OnClickIdle()
        {
            var logic = XSUG.GetBattleLogic();
            var unit = logic.UnitMgr.ActionUnit;
            if (unit == null)
                return;

            unit.SetActived();
            logic.Change(new PhaseChooseUnit());
        }
        /************************* 按钮回调  end  ***********************/
    }
}
