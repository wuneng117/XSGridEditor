using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 
    /// 战斗管理
    /// 继承BattleFSMBase，修改回合状态
    /// </summary>
    public class BattleLogic : BattleFSMBase
    {
        /************************* 变量 begin ***********************/

        /// <summary> grid 助手 </summary>
        public XSGridHelper GridHelper { get; }

        // public XSUnitMgr UnitMgr { get; protected set; }

        /// <summary> 战斗单位数组 </summary>
        public List<Unit> UnitList { get; } = new List<Unit>();

        /// <summary> 死亡单位数组 </summary>
        public List<Unit> DeadUnitList { get; } = new List<Unit>();

        /// <summary> 出击单位数组，用来放置到地图上出击 </summary>
        public List<Unit> PrepareUnitList { get; } = new List<Unit>();

        /// <summary> 当前行动的unit </summary>
        public Unit ActionUnit { get; private set; }

        /************************* 变量  end  ***********************/

        public BattleLogic()
        {
            this.GridHelper = Component.FindObjectOfType<XSGridHelper>();
            Debug.Assert(this.GridHelper, "找不到XSGridHelper");

            // this.UnitMgr = new XSUnitMgr(this.GridHelper);
            this.CreateUnitList(this.GridHelper.GetUnitDataList());
        }

        public void CreateUnitList(List<XSIUnitNode> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0)
            {
                return;
            }

            // traverse
            nodeList.ForEach(unitNode =>
            {
                unitNode.AddBoxCollider();
                var unit = unitNode.GetUnit();
                if (unit != null)
                {
                    this.UnitList.Add(unit);
                }
            });
        }
        /// <summary>
        /// 设置行动的unit
        /// </summary>
        /// <param name="tile">要设置的unit所在tile</param>
        /// <param name="type">检查势力是否是当前行动</param>
        public bool SetActionUnit(XSTile tile, GroupType type)
        {
            if (tile == null)
            {
                return false;
            }

            var unit = this.GetUnitByCellPosition(tile.TilePos);
            if (unit == null || unit.Group != type || unit.IsActived())
                return false;
            this.ActionUnit = unit;
            return true;
        }

        /// <summary>
        /// 设置行动的unit
        /// </summary>
        /// <param name="unit">要设置的unit</param>
        /// <param name="type">检查势力是否是当前行动</param>
        public bool SetActionUnit(Unit unit, GroupType type)
        {
            if (unit == null || unit.Group != type || unit.IsActived())
                return false;
            this.ActionUnit = unit;
            return true;
        }

        /// <summary> 清除action的unit </summary>
        public void ClearActionUnit() => this.ActionUnit = null;

        /// <summary> 获取所有单位 {unit的tile坐标，unit} 字典 </summary>
        public Dictionary<XSTile, Unit> GetTileUnitDict()
        {
            var ret = this.UnitList.Where(unit => XSInstance.GridMgr.HasXSTileByWorldPos(unit.GetPosition()))
                                   .ToDictionary(unit => XSInstance.GridMgr.GetXSTileByWorldPos(unit.GetPosition()));
            return ret;
        }

        /// <summary> 获取自己unit </summary>
        public List<Unit> GetSelfUnitList() => this.UnitList.FindAll(unit => unit.Group == GroupType.Self);

        /// <summary> 获取敌人unit </summary>
        public List<Unit> GetEnemyUnitList() => this.UnitList.FindAll(unit => unit.Group == GroupType.Enemy);

        /// <summary> 获取友方unit </summary>
        public List<Unit> GetFriendUnitList() => this.UnitList.FindAll(unit => unit.Group == GroupType.NpcFriend);

        /// <summary>
        /// 根据世界坐标返回unit
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        public Unit GetUnitByWorldPosition(Vector3 worldPos)
        {
            var tile = XSInstance.GridMgr.GetXSTileByWorldPos(worldPos);
            return this.GetUnitByTile(tile);
        }

        /// <summary>
        /// 根据世界坐标返回unit
        /// </summary>
        /// <param name="cellPos">网格坐标</param>
        public Unit GetUnitByCellPosition(Vector3Int cellPos)
        {
            if (XSInstance.GridMgr.TryGetXSTile(cellPos, out var tile))
            {
                return this.GetUnitByTile(tile);
            }
            else
            {
                return null;
            }
        }

        private Unit GetUnitByTile(XSTile tile)
        {
            if (tile == null)
                return null;

            var tileUnitDict = this.GetTileUnitDict();
            return tileUnitDict.ContainsKey(tile) ? tileUnitDict[tile] : null;
        }

        public void TurnBegin() => this.Change(new PhaseTurnBegin());

        /// <summary> 统一处理Change传递的第二个参数就是自己 </summary>
        public void Change(IPhaseBase nextPhase) => this.Change(nextPhase, this);

        /// <summary> 统一处理Update传递的第二个参数就是自己 </summary>
        public void Update() => this.Update(this);
    }
}