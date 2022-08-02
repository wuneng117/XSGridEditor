/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/9
/// @Description: tile 上放置的 object 数据结构
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 上放置的 object 数据结构 </summary>
    public class XSUnitData : MonoBehaviour
    {
        /// <summary> 用字符串表示id比较通用 </summary>
        public string Id = "-1";

        public virtual GameObject GetGameObj()
        {
            return null;
        }

        Bounds GetMaxBounds()
        {
            var renderers = this.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0) 
                return new Bounds(this.transform.position, Vector3.zero);

            var ret = renderers[0].bounds;
            foreach (Renderer r in renderers)
                ret.Encapsulate(r.bounds);
            return ret;
        }
    }
}
