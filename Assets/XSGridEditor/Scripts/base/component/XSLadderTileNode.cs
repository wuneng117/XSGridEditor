/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 节点上的组件
/// </summary>

using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSLadderTileNode : XSTileNode
    {
        /// <summary> 移动消耗 </summary>
        [SerializeField]
        protected XSGridDefine.XSLadderTileType ladderType = XSGridDefine.XSLadderTileType.UpDown;

        public XSGridDefine.XSLadderTileType LadderType { get => this.ladderType; }

        public override XSTile CreateXSTile(Vector3Int tilePos) => new XSLadderTile(tilePos, this.WorldPos, this.Cost, this, this.ladderType);
    }
}
