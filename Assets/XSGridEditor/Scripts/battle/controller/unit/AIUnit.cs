using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 电脑控制的战斗单位 </summary>
    public class AIUnit : Unit
    {
        // /// <summary> 实际坐标 </summary>
        // public override Vector3 GetPosition() => this.Node.transform.position;

        // /// <summary> unity的unit脚本 </summary>
        // public UnitNode Node { get; }

        // public Unit(Role role, GroupType group, UnitNode node) : base(role, group)
        // {
        //     this.Node = node ?? throw new System.ArgumentNullException(nameof(node));
        // }

        // /// <summary> 设置网格坐标 </summary>
        // override public void SetCellPosition(PathFinderTile tile)
        // {
        //     base.SetCellPosition(tile);
        //     var transform = this.Node.transform;
        //     var logic = XSUG.GetBattleLogic();
        //     transform.position = XSInstance.GridMgr.TileToWorld(tile.CellPos);
        // }

        // /// <summary>
        // /// 移动到指定位置
        // /// </summary>
        // /// <param name="path">移动路径</param>
        // public bool WalkTo(Vector3 worldDestPos)
        // {
        //     var path = this.FindPath(worldDestPos);
        //     if (path.Count == 0)
        //         return false;

        //     this.Node.WalkTo(path);
        //     return true;
        // }
        public AIUnit(Role role, GroupType group, XSIUnitNode node) : base(role, group, node)
        {
        }

        /************************* ai计算 begin ***********************/
        // public void 
        /************************* ai计算  end  ***********************/
    }
}