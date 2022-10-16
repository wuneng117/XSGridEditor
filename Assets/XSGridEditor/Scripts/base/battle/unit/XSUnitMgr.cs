
/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-10-04 17:32:46
/// @Description: 
/// </summary>
using System.Collections.Generic;
using System.Linq;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public class XSUnitMgr : XSNodeMgr<Unit>
    {
        /************************* 变量 begin ***********************/
        /// <summary> 死亡单位数组 </summary>
        public Dictionary<Vector3Int, Unit> DeadUnitList { get; } = new Dictionary<Vector3Int, Unit>();

        /// <summary> 出击单位数组，用来放置到地图上出击 </summary>
        public Dictionary<Vector3Int, Unit> PrepareUnitList { get; } = new Dictionary<Vector3Int, Unit>();

        /// <summary> 当前行动的unit </summary>
        public Unit ActionUnit { get; private set; }

        /************************* 变量 end ***********************/

        public XSUnitMgr()
        {
            this.CreateUnitList(XSU.GridHelper.GetUnitNodeList());
        }

        protected void CreateUnitList(List<XSIUnitNode> nodeList)
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
                    this.Add(unit);
                }
            });
        }

        public virtual void SetUnitDied(Unit unit)
        {
            this.Remove(unit);
            var tilePos = XSU.GridMgr.WorldToTile(unit.WorldPos);
            this.DeadUnitList.Add(tilePos, unit);
        }

        /// <summary> 获取所有单位 {unit的tile坐标，unit} 字典 </summary>
        public Dictionary<XSTile, Unit> GetTileUnitDict()
        {
            var ret = this.Dict.Where(pair => XSU.GridMgr.HasXSTileByWorldPos(pair.Value.WorldPos))
                               .ToDictionary(pair => XSU.GridMgr.GetXSTileByWorldPos(pair.Value.WorldPos), pair => pair.Value);
            return ret;
        }

        /// <summary> 获取自己unit </summary>
        public List<Unit> GetSelfUnitList() => this.Dict.Where(pair => pair.Value.Group == GroupType.Self).Select(pair => pair.Value).ToList();

        /// <summary> 获取敌人unit </summary>
        public List<Unit> GetEnemyUnitList() => this.Dict.Where(pair => pair.Value.Group == GroupType.Enemy).Select(pair => pair.Value).ToList();

        /// <summary> 获取友方unit </summary>
        public List<Unit> GetFriendUnitList() => this.Dict.Where(pair => pair.Value.Group == GroupType.NpcFriend).Select(pair => pair.Value).ToList();

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

        /// <summary>
        /// 根据世界坐标返回unit
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        public Unit GetUnitByWorldPosition(Vector3 worldPos)
        {
            var tile = XSU.GridMgr.GetXSTileByWorldPos(worldPos);
            return this.GetUnitByTile(tile);
        }

        /// <summary>
        /// 根据世界坐标返回unit
        /// </summary>
        /// <param name="cellPos">网格坐标</param>
        public Unit GetUnitByCellPosition(Vector3Int cellPos)
        {
            if (XSU.GridMgr.TryGetXSTile(cellPos, out var tile))
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
    }
}