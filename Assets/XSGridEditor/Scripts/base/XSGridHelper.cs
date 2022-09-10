/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 辅助类
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 辅助类 </summary>
    [ExecuteInEditMode]
    public class XSGridHelper : MonoBehaviour
    {
        /// <summary> tile根节点 </summary>
        [SerializeField]
        protected Transform tileRoot;
        public Transform TileRoot { get => tileRoot; }

        /// <summary> unit根节点 </summary>
        [SerializeField]
        private Transform unitRoot;
        public Transform UnitRoot { get => unitRoot; }

        /// <summary> 移动范围用的 prefab </summary>
        [SerializeField]
        private GameObject moveTilePrefab;
        public GameObject MoveTilePrefab { get => moveTilePrefab; }

        /// <summary> tile的最高高度差，大于这个值表示tile不联通 </summary>
        [SerializeField]
        protected int tileOffYMax = 1;
        public int TileOffYMax { get => this.tileOffYMax; }

        /// <summary> tile 相对于其他物体的抬高高度，防止重叠导致显示问题 </summary>
        protected float Precision = 0.01f;

        /// <summary> 射线检测的高度 </summary>
        protected float TopDistance = 10f;

        /// <summary> 获取所有 XSITileNode 节点 </summary>
        public List<XSITileNode> GetTileNodeList() => this.TileRoot.GetComponentsInChildren<XSITileNode>().ToList();

        /// <summary> 获取所有 XSObjectData 节点 </summary>
        public List<XSIUnitNode> GetUnitDataList() => this.UnitRoot.GetComponentsInChildren<XSIUnitNode>().ToList();

        public virtual Bounds GetBounds()
        {
            var ret = new Bounds();
            if (XSUnityUtils.IsEditor())
            {
                return ret;
            }

            var tiles = this.GetTileNodeList();
            if (tiles.Count == 0)
            {
                return ret;
            }

            var collider = ((XSTileNode)tiles[0]).GetComponent<BoxCollider>();
            if (collider == null)
            {
                return ret;
            }

            var bound = collider.bounds;
            tiles.ForEach(tile =>
            {
                var col = ((XSTileNode)tile).GetComponent<BoxCollider>();
                if (col)
                {
                    bound.Encapsulate(col.bounds);
                }
            });

            return bound;
        }

        /// <summary>
        /// 调整 objTransform 的高度到障碍物的顶端。把 objTransform 中心点抬高100，再检测100以内有没有碰撞物，碰到的话就把 objTransform 的高度设置到碰撞点的位置
        /// tips：游戏时整个场景会有个大的 boxcollider 作为摄像机移动范围，这个 collider需要排除掉，所以高度设置为10（低一点）
        /// </summary>
        public virtual bool SetTransToTopTerrain(Transform objTransform, bool closeUnit)
        {
            // 隐藏objTransform ，防止射线碰到 objTransform
            objTransform.gameObject.SetActive(false);
            // 隐藏所有unit，防止参与射线检测
            if (closeUnit)
            {
                XSUnityUtils.ActionChildren(this.unitRoot?.gameObject, (child) => child.SetActive(false));
            }
            var pos = objTransform.position;
            // 射线发射点，抬高 objTransform 以后的中心点
            var top = new Vector3(pos.x, this.TopDistance, pos.z);
            var ray = new Ray(top, Vector3.down);
            RaycastHit hitInfo;
            // 这里检测 ray 和其他物体的碰撞
            // ray 位置和射线方向，位置是 抬高TopDistance后的坐标，方向垂直向下
            var ret = Physics.Raycast(ray, out hitInfo);
            if (ret)
            {
                var newPos = hitInfo.point + new Vector3(0, this.Precision, 0);
                objTransform.position = newPos;
            }
            // 如果贴不到任何东西，那就设置localpos的高度y为0
            else if (objTransform.localPosition.y != 0)
            {
                ret = true;
                objTransform.localPosition = new Vector3(objTransform.localPosition.x, 0, objTransform.localPosition.z);
            }

            //激活 objTransform
            objTransform.gameObject.SetActive(true);
            // 显示所有unit，防止参与射线检测
            if (closeUnit)
            {
                XSUnityUtils.ActionChildren(this.unitRoot?.gameObject, (child) => child.SetActive(true));
            }
            return ret;
        }
    }
}
