/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 计算哪个敌人开始行动
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 计算哪个敌人开始行动
    /// </summary>
    public class AIChooseUnit : PhaseBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            // 要选择行动的unit，把之前的清除下
            XSUG.GetBattleLogic().ClearActionUnit();
            var nextUnit = logic.GetEnemyUnitList().Find(enemy => logic.SetActionUnit(enemy, GroupType.Enemy));
            // 没有unit可以行动了
            if (nextUnit == null)
                logic.Change(new AITurnEnd());
            else
                logic.Change(new AIChooseAction());
        }
    }
}
