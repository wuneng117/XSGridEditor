/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 辅助类
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary> 辅助类 </summary>
    [ExecuteInEditMode]
    public class XSGridHelper : MonoBehaviour
    {
        /// <summary> 移动范围用的图片 </summary>
        public Sprite TileSpriteMove = null;

        /// <summary> 攻击范围用的图片 </summary>
        public Sprite TileSpriteAttack = null;
        /// <summary> 攻击效果范围用的图片 </summary>
        public Sprite TileSpriteAttackEffect = null;

        /// <summary> tile 的prefab 文件 </summary>
        public GameObject TilePrefab = null;

        /// <summary> 获取所有 XSTileData 节点 </summary>
        public XSTileData[] GetTileDataArray()
        {
            XSTileData[] ret = { };
            ret = this.GetComponentsInChildren<XSTileData>();
            return ret;
        }

        public Bounds GetBounds()
        {
            var tiles = this.GetTileDataArray();
            if (tiles.Length == 0)
                return new Bounds();

            var bound = tiles[0].GetComponent<BoxCollider>().bounds;
            foreach (var tile in tiles)
                bound.Encapsulate(tile.GetComponent<BoxCollider>().bounds);
            return bound;
        }
    }
}