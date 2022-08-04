using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary>  </summary>
    public class GridShowMgr
    {
        /************************* 变量 begin ***********************/
        // /// <summary> 所有网格显示 </summary>
        // private GridShowRegion ShowRegion { get; set; }

        /// <summary> 移动范围显示 </summary>
        private GridShowRegion MoveShowRegion { get; set; }

        /// <summary> 攻击范围显示 </summary>
        private GridShowRegion AttackShowRegion { get; set; }

        /// <summary> 攻击效果范围显示 </summary>
        private GridShowRegion AttackEffectShowRegion { get; set; }

        /// <summary> 网格管理 </summary>
        public IGridMgr mgr { get; }

        /************************* 变量  end  ***********************/

        /// <summary> 初始化网格的贴花管理 </summary>
        public GridShowMgr(IGridMgr mgr, XSGridHelper gridHelper)
        {
            this.mgr = mgr;

            if (gridHelper)
            {
                Func<Vector3Int, Vector3> tileToWorld = this.mgr.TileToWorld;
                this.MoveShowRegion = new GridShowRegion(XSGridDefine.SCENE_GRID_MOVE, gridHelper.TileSpriteMove, gridHelper.TilePrefab, tileToWorld, 10);
                this.AttackShowRegion = new GridShowRegion(XSGridDefine.SCENE_GRID_ATTACK_RANGE, gridHelper.TileSpriteAttack, gridHelper.TilePrefab, tileToWorld, 20);
                this.AttackEffectShowRegion = new GridShowRegion(XSGridDefine.SCENE_GRID_ATTACK_EFFECT_RANGE, gridHelper.TileSpriteAttackEffect, gridHelper.TilePrefab, tileToWorld, 30);
                // var ret = this.mgr.Data.GetVaildTileDataList().Select(tileData => new Vector3Int(tileData.XIndex, tileData.YIndex, tileData.ZIndex)).ToList();
                // if (XSU.GetBattleDebugCfg().isShowCellPos)
                // {
                //     this.ShowRegion = new GridShowRegion(XSGridDefine.SCENE_GRID_BASE, BattleRes.GRID_BASE, tileToWorld);
                //     this.ShowRegion.ShowRegion(ret);
                // }
            }

        }

        /// <summary>
        /// 测试显示范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        public void TestShowRegion(List<Vector3Int> list) => this.MoveShowRegion.ShowRegion(list);

        /// <summary>
        /// 显示单位移动范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        public List<Vector3Int> ShowMoveRegion(XSUnitData unit)
        {
            var moveRegion = unit.GetMoveRegion();
            this.MoveShowRegion.ShowRegion(moveRegion);
            return moveRegion;
        }

        /// <summary> 清除单位移动范围 </summary>
        public void ClearMoveRegion() => this.MoveShowRegion.ClearRegion();

        // /// <summary>
        // /// 显示单位攻击范围
        // /// </summary>
        // /// <param name="unit">显示范围的单位</param>
        // /// <param name="skill">显示范围的技能</param>
        // public List<Vector3Int> ShowAttackRegion(Unit unit, SkillBase skill)
        // {
        //     if (unit == null)
        //         return new List<Vector3Int>();

        //     var gridMgr = this.mgr;
        //     var srcTile = gridMgr.GetTile(unit.Node.transform.position);
        //     if (srcTile == null)
        //         return new List<Vector3Int>();

        //     var attackRegion = skill.Trigger.GetAttackRegion(gridMgr, srcTile);
        //     this.AttackShowRegion.ShowRegion(attackRegion);
        //     return attackRegion;
        // }

        // /// <summary> 清除单位攻击范围 </summary>
        // public void ClearAttackRegion() => this.AttackShowRegion.ClearRegion();

        // /// <summary>
        // /// 显示单位攻击效果范围
        // /// </summary>
        // /// <param name="unit">显示范围的单位</param>
        // /// <param name="skill">显示范围的技能</param>
        // /// <param name="skill">当前鼠标所在的世界坐标</param>
        // public void ShowAttackEffectRegion(Unit unit, SkillBase skill, Vector3 worldPos)
        // {
        //     if (unit == null)
        //         return;

        //     var gridMgr = this.mgr;
        //     var tile = gridMgr.GetTile(worldPos);
        //     if (tile == null)
        //         return;

        //     var srcTile = gridMgr.GetTile(unit.Node.transform.position);
        //     if (srcTile == null)
        //         return;

        //     var moveRegion = skill.Trigger.GetAttackEffectRegion(tile, srcTile, gridMgr.PathFinder.TileDict);
        //     this.AttackEffectShowRegion.ShowRegion(moveRegion);
        // }

        /// <summary> 清除单位攻击效果范围 </summary>
        public void ClearAttackEffectRegion() => this.AttackEffectShowRegion.ClearRegion();
    }
}