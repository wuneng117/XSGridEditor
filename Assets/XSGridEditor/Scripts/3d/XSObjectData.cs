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
    [ExecuteInEditMode]
    public class XSObjectData : MonoBehaviour
    {
        /// <summary> 用字符串表示id比较通用 </summary>
        public string Id = "-1";
        // Start is called before the first frame update
        protected Vector3 PrevPos;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                // UnityGameUtils.Log("XSObjectData Update");
                var gridMgr = new GridMgr();    // TODO 多次Update时导致GridMgr多次初始化，可以优化下，不过不会造成性能问题，因为每帧调用最多1次
                var pos = gridMgr.WorldToTileCenterWorld(this.transform.position);
                // zero 表示返回的为空，tile获取有问题
                if (pos != Vector3.zero)
                {
                    this.transform.position = pos;
                    this.PrevPos = pos;
                }
                else
                {
                    this.transform.position = this.PrevPos;
                }
            }
        }

        public virtual GameObject GetGameObj()
        {
            return null;
        }

        virtual protected void AddGameObj()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                var obj = this.GetGameObj();
                if (obj)
                {
                    UnityUtils.RemoveChildren(this.gameObject);
                    obj.transform.parent = this.transform;
                }
            }
        }
    }
}
