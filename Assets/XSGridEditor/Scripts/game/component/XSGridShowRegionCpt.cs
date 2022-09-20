/// <summary>
/// @Author: xiaoshi
/// @Date:2021/5/26
/// @Description: used with GridMgr, show range
/// </summary>
using System;
using System.Collections.Generic;
using UnityEngine;
namespace XSSLG
{
    /// <summary> used with GridMgr, show range </summary>
    public class XSGridShowRegionCpt : MonoBehaviour, XSIGridShowRegion
    {
        /************************* variable begin ***********************/
        /// <summary> prefab to show range </summary>
        public GameObject Prefab { get; set; }

        /// <summary> sprite sort order </summary>
        protected int SortOrder { get; set; }

        /************************* variable  end  ***********************/

        public static XSGridShowRegionCpt Create(string rootPath, GameObject moveTilePrefab, int sortOrder)
        {
            var parent = XSInstance.Instance.GridHelper?.transform;
            if (parent == null)
            {
                return null;
            }

            var node = new GameObject(rootPath).transform;
            node.SetParent(parent);
            var showRegion = node.gameObject.AddComponent<XSGridShowRegionCpt>();
            showRegion.Init(moveTilePrefab, sortOrder);
            return showRegion;
        }

        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="sortOrder"></param>
        public virtual void Init(GameObject prefab, int sortOrder) => (this.Prefab, this.SortOrder) = (prefab, sortOrder);

        /// <summary>
        /// show range
        /// </summary>
        /// <param name="worldPosList">a list of world position to show range </param>
        public virtual void ShowRegion(List<Vector3> worldPosList)
        {
            if (this.Prefab == null)
            {
                return;
            }

            XSUnityUtils.ActionChildren(XSInstance.Instance.GridHelper.UnitRoot?.gameObject, (child) => child.SetActive(false));

            worldPosList.ForEach(pos =>
            {
                var obj = GameObject.Instantiate(this.Prefab, this.transform);
                if (obj == null)
                {
                    return;
                }

                // Set layer to default. Do not block the raycast
                obj.layer = LayerMask.NameToLayer("Default");
                obj.transform.position = pos;
                XSInstance.Instance.GridHelper.SetTransToTopTerrain(obj.transform, false);

                var spr = obj.GetComponentInChildren<SpriteRenderer>();
                spr.sortingOrder = this.SortOrder;
            });
            XSUnityUtils.ActionChildren(XSInstance.Instance.GridHelper.UnitRoot?.gameObject, (child) => child.SetActive(true));
        }

        /// <summary> clear range </summary>
        public virtual void ClearRegion() => XSUG.RemoveChildren(this.transform.gameObject);

        public virtual bool IsNull() => this == null;
    }
}