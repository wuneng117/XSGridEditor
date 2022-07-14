/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/9
/// @Description: tile 上放置的 object 数据结构
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> XSObjectData 的编辑器操作 </summary>
    [RequireComponent(typeof(XSObjectData))]
    [ExecuteInEditMode]
    public class XSObjectDataEditMode : MonoBehaviour
    {
        /// <summary> 编辑器模式下记录上一次的坐标 </summary>
        protected Vector3 PrevPos { get; set; }

        void Start()
        {
            if (XSU.IsEditor())
                this.AddGameObj();
            else
                this.enabled = false;
        }

        private void OnDestroy()
        {
            if (XSU.IsEditor())
            {
                UnityUtils.ActionChildren(this.gameObject, (child) =>
                {
                    if (!PrefabUtility.IsPartOfAnyPrefab(child))
                        GameObject.DestroyImmediate(child);
                });
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (XSU.IsEditor())
            {
                // XSU.Log("XSObjectData Update");
                if (XSGridHelperEditMode.Instance)
                {
                    var gridMgr = XSGridHelperEditMode.Instance.GridMgr;    // TODO 多次Update时导致GridMgr多次初始化，可以优化下，不过不会造成性能问题，因为每帧调用最多1次
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
        }

        virtual protected void AddGameObj()
        {
            if (XSU.IsEditor())
            {
                var objData = this.GetComponent<XSObjectData>();
                var obj = objData.GetGameObj();
                if (obj)
                {
                    UnityUtils.ActionChildren(this.gameObject, (child) =>
                    {
                        if (PrefabUtility.IsPartOfAnyPrefab(child))
                            child.SetActive(false);
                        else
                             GameObject.DestroyImmediate(child);
                    });
                    obj.transform.parent = this.transform;
                }
            }
        }
    }
}
