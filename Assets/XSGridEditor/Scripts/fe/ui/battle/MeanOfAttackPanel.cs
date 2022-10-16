/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/3
/// @Description: unit选择武器/魔法进行攻击
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;

namespace XSSLG
{
    public class MeanOfAttackPanel : MonoBehaviour
    {
        private SkillBase Skill { get; set; }

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

        /// <summary>
        /// 根据技能显示对应武器/魔法
        /// </summary>
        /// <param name="skill">对应的技能</param>
        public void Init(SkillBase skill)
        {
            this.Skill = skill;
        }

        /************************* 按钮回调 begin ***********************/

        /// <summary> 点击 </summary>
        public void OnClick()
        {
            // 延迟一帧执行，因为点击比update执行要快，如果直接执行，这帧还会调用下一个phase的update
            this.StartCoroutine(this.ChangePhase());
        }

        private IEnumerator ChangePhase()
        {
            yield return new WaitForEndOfFrame();
            XSU.GetBattleLogic().Change(new PhaseChooseAtk(this.Skill));
        }

        /************************* 按钮回调  end  ***********************/
    }
}
