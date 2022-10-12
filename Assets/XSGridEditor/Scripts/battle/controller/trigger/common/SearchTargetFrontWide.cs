/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/8
/// @Description: 搜索前方敌人，宽的范围
/// </summary>
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 搜索前方敌人，宽的范围 </summary>
    public class SearchTargetFrontWide : SearchTargetFront
    {
        public SearchTargetFrontWide(SearchStruct searchStruct, int length) : base(searchStruct, length) { }

        /// <summary>
        /// 获取攻击效果范围的格子，宽的范围搜索
        /// </summary>
        /// <param name="tile">技能释放的原点，也可以说是鼠标点击的地点</param>
        /// <param name="srcTile">技能释放者所在位置</param>
        /// <param name="cellPosList">以网格坐标存储所有tile</param>
        /// <returns></returns>
        public override List<XSTile> GetAttackEffectRegion(Vector3Int tile, Vector3Int srcTile)
        {
            var posList = new List<Vector3Int>();
            var nor = tile - srcTile;
            if (nor.x != 0)
            {
                posList.Add(tile + new Vector3Int(0, 1, 0));
                posList.Add(tile + new Vector3Int(0, -1, 0));
            }
            else
            {
                posList.Add(tile + new Vector3Int(1, 0, 0));
                posList.Add(tile + new Vector3Int(-1, 0, 0));
            }

            var ret = new List<XSTile>();
            posList.ForEach(tilePos =>
            {
                // 确实有这个格子再加
                if (XSU.GridMgr.TryGetXSTile(tilePos, out var tile))
                    ret.Add(tile);
            });

            return ret;
        }
    }
}