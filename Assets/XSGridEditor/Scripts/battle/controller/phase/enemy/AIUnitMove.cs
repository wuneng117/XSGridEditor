using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态选择移动的位置
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 选择移动的位置
    /// </summary>
    public class AIUnitMove : PhaseChooseMoveBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            var actionUnit = logic.ActionUnit;
            // 返回所有ActionUnit走到我方unit的路径（如果有路径）
            var srcWorldPos = actionUnit.GetPosition();
            var srcTile = XSU.GridMgr.GetXSTileByWorldPos(srcWorldPos);

            var pathList = logic.GetSelfUnitList()
                .Select(unit =>
                {
                    var destWorldPos = unit.GetPosition();
                    var destTile = XSU.GridMgr.GetXSTileByWorldPos(destWorldPos);
                    return XSU.GridMgr.FindPath(srcTile, destTile);
                })
                .Where(list => list.Count > 0).ToList();

            this.TryToWalk(logic, actionUnit, pathList);
        }

        private bool TryToWalk<T>(T logic, Unit actionUnit, List<List<Vector3>> pathList) where T : BattleLogic
        {
            if (pathList.Count == 0)
                return false;

            var path = pathList[Random.Range(0, pathList.Count)];
            // 如果超过移动范围，只走一半就行
            // 注意0是终点，path[count-1]是第一格
            var count = Mathf.Max(0, path.Count - actionUnit.GetStat().GetMov().GetFinal());
            for (; count < path.Count; count++)
            {
                if (logic.GetUnitByWorldPosition(path[count]) == null)
                    break;
            }

            if (count == path.Count)
                return false;

            path.RemoveRange(0, count);
            logic.ActionUnit.Node.WalkTo(path);
            return true;
        }

        public override void Update<T>(T logic)
        {
            base.Update(logic);
            PhaseUtils.CheckIsMoving(this, logic, new AIChooseAction());
        }
    }
}