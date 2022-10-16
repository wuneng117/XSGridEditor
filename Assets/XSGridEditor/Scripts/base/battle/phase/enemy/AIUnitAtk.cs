using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态单位攻击中
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// AI单位攻击中
    /// </summary>
    public class AIUnitAtk : PhaseUnitAtkBase
    {
        public AIUnitAtk(SkillBase skill, XSTile tile) : base(skill, tile) { }
        protected override PhaseBase GetNextPhase() => new AIChooseUnit();
        
        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            XSU.CameraCanFreeMove(false);    // ai下摄像机就是不能移动
            logic.UnitMgr.ActionUnit.SetActived();
        }
    }
}