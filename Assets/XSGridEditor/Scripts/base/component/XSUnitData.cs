/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/9
/// @Description: tile 上放置的 object 数据结构
/// </summary>
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 上放置的 object 数据结构 </summary>
    public class XSUnitData : MonoBehaviour, XSIUnitNode
    {
        /// <summary> 用字符串表示id比较通用 </summary>
        [SerializeField]
        private string id = "-1";
        public string Id { get => id; set => id = value; }

        [SerializeField]
        private int move = 6;
        public int Move { get => move; set => move = value; }

        public Dictionary<Vector3, List<Vector3>> CachedPaths { get; private set; }

        public Vector3 WorldPos { get => this.transform.position; }

        public void AddBoxCollider()
        {
            var collider = this.gameObject.AddComponent<BoxCollider>();
            var bounds = this.GetMaxBounds();
            collider.bounds.SetMinMax(bounds.min, bounds.max);
            collider.center = collider.transform.InverseTransformPoint(bounds.center);
            collider.size = bounds.size;
        }

        protected Bounds GetMaxBounds()
        {
            var renderers = this.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
                return new Bounds();

            var ret = renderers[0].bounds;
            foreach (Renderer r in renderers)
                ret.Encapsulate(r.bounds);
            return ret;
        }

        /// <summary>
        /// 获取移动范围
        /// </summary>
        /// <returns></returns>
        public List<Vector3> GetMoveRegion()
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            var srcTile = gridMgr.GetXSTile(this.transform.position);
            // 缓存起来哈
            this.CachedPaths = gridMgr.FindAllPath(srcTile, this.Move);
            // 把this.CachedPaths累加起来
            var ret = this.CachedPaths.Aggregate(new List<Vector3>(), (ret, pair) =>
            {
                // 去重
                ret.AddRange(pair.Value.Distinct());
                return ret;
            }).Distinct().ToList(); // 去重
            return ret;
        }
    }
}
