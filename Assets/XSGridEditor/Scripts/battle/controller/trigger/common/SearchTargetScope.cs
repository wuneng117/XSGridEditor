/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/8
/// @Description: 搜索周围敌人
/// </summary>
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 搜索周围敌人 </summary>
    public class SearchTargetScope : SearchTargetBase
    {
        /************************* 变量 begin ***********************/
        /// <summary> 周围多少范围内 </summary>
        private int Scope { get; }
        /************************* 变量  end  ***********************/

        public SearchTargetScope(TriggerDataSearchStruct searchStruct, int scope) : base(searchStruct)
        {
            this.Scope = scope;
        }

        /// <summary>
        /// 获取攻击范围
        /// </summary>
        /// <param name="srcTile">技能释放者所在位置</param>
        /// <param name="gridMgr">参数传递下就不用重新获取了</param>
        /// <returns></returns>
        public override List<XSTile> GetAttackRegion(XSIGridMgr gridMgr, XSTile srcTile)
        {
            var ret = new List<XSTile>();
            //TODO 可以做个优化，如果xy要搜索的格子数大于所有unit，可以遍历unit
            var minSqr = this.SearchStruct.Min * this.SearchStruct.Min;
            var maxSqr = this.SearchStruct.Max * this.SearchStruct.Max;
            for (var x = 0; x <= this.SearchStruct.Max; x++)
            {
                for (var y = 0; y <= this.SearchStruct.Max; y++)
                {
                    var distanceSqr = x * x + y * y;
                    if (distanceSqr > maxSqr || distanceSqr < minSqr)
                        continue;

                    var pos = srcTile.TilePos + new Vector3Int(x, y, 0);
                    if (gridMgr.TryGetXSTile(pos, out var tile))
                        ret.Add(tile);
                }
            }
            return ret;
        }

        /// <summary>
        /// 获取攻击效果范围的格子，比如小十字，Scope为1，那么距离小于1的格子都加进去
        /// </summary>
        /// <param name="cellPos">技能释放的原点，也可以说是鼠标点击的地点</param>
        /// <param name="srcPos">技能释放者所在位置</param>
        /// <returns></returns>
        public override List<XSTile> GetAttackEffectRegion(Vector3Int cellPos, Vector3Int srcPos)
        {
            var ret = new List<XSTile>();
            var distanceSqr = this.Scope * this.Scope;
            for (var x = 0; x <= this.Scope; x++)
                for (var y = 0; y <= this.Scope; y++)
                    if (x * x + y * y <= distanceSqr)
                    {
                        var cell = cellPos + new Vector3Int(x, y, 0);
                        // 确实有这个格子再加
                        if (XSInstance.GridMgr.TryGetXSTile(cell, out var tile))
                            ret.Add(tile);
                    }
            return ret;
        }
    }
}