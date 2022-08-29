/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/23
/// @Description: 这是一个正方形网格寻路模块，先将网格数据全部转成XSTile，再调用寻路
/// </summary>
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;

namespace XSSLG
{
    using TileFunc = Func<Vector3Int, bool>;

    /// <summary> 用于计算的结构 </summary>
    public class XSStepTile : XSTile
    {
        public virtual bool PassNearRule(XSTile tile, int tileYMax)
        {
            //TODO
            return Mathf.Abs(tile.WorldPos.y, this.WorldPos.y) > helper.TileOffYMax;
        }
    }
}