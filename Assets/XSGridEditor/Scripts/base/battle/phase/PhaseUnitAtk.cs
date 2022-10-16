using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态单位攻击中
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 玩家单位攻击中
    /// </summary>
    public class PhaseUnitAtk : PhaseUnitAtkBase
    {
        public PhaseUnitAtk(SkillBase skill, XSTile tile) : base(skill, tile) { }
        protected override PhaseBase GetNextPhase() => new PhaseUnitMenu();
    }
}