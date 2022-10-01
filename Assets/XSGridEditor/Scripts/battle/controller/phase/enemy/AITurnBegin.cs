using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: AI战斗状态回合开始
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// AI的回合开始，通知AI回合开始，AI做一些回合开始的操作
    /// </summary>
    public class AITurnBegin : PhaseBase
    {
        private CustomScheduler Scheduler { get; set; } = new CustomScheduler();

        /// <summary> 状态进入 </summary>
        public override void OnEnter<T>(T logic)
        {
            // 通知敌人单位onTurnStart
            logic.GetEnemyUnitList().ForEach(unit => unit.OnTurnStart());
            XSUG.CameraCanFreeMove(false);

            var battleNode = XSUG.GetBattleNode();
            battleNode.OpenTurnChange(GroupType.Enemy);

            this.Scheduler.ScheduleOnce(() =>
            {
                var battleNode = XSUG.GetBattleNode();
                battleNode.CloseTurnChange(GroupType.Enemy);
                // 切换到选择单位行动
                logic.Change(new AIChooseUnit(), logic);
            }, 2);
        }

        public override void Update<T>(T logic)
        {
            base.Update(logic);
            this.Scheduler.Update(Time.deltaTime);
        }
    }
}
