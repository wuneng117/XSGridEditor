/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: Auxiliary class
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> Auxiliary class </summary>
    [ExecuteInEditMode]
    public class XSGridHelper : MonoBehaviour
    {
        [SerializeField]
        protected Transform tileRoot;
        public Transform TileRoot { get => tileRoot; }

        [SerializeField]
        private Transform unitRoot;
        public Transform UnitRoot { get => unitRoot; }

        /// <summary> prefab for moving range </summary>
        [SerializeField]
        private GameObject moveTilePrefab;
        public GameObject MoveTilePrefab { get => moveTilePrefab; }

        /// <summary> The maximum height offset between two near tiles, greater than this value means that the tile is not connected </summary>
        [SerializeField]
        protected int tileOffYMax = 1;
        public int TileOffYMax { get => this.tileOffYMax; }

        /// <summary> The raised height of the tile relative to other objects to prevent overlapping and causing display problems </summary>
        protected float Precision = 0.01f;

        /// <summary> height of raycast </summary>
        protected float TopDistance = 10f;

        /// <summary> get all XSITileNode </summary>
        public List<XSITileNode> GetTileNodeList() => this.TileRoot.GetComponentsInChildren<XSITileNode>().ToList();

        /// <summary> get all XSUnitNode </summary>
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
        /// Adjust the height of the objTransform to the top of the obstacle. Raise the center point of objTransform by 100, and then 
        /// check if there is a collision within 100. If it hits, set the height of objTransform to the position of the collision point
        /// tips：During the game, the entire scene will have a large boxcollider as the camera movement range. This collider needs to be excluded, so the height is set to 10 (lower)
        /// </summary>
        public virtual bool SetTransToTopTerrain(Transform objTransform, bool closeUnit)
        {
            // hide objTransform ，prevent ray from hitting objTransform
            objTransform.gameObject.SetActive(false);
            // hide all unit ，prevent ray from hitting unit
            if (closeUnit)
            {
                XSUnityUtils.ActionChildren(this.unitRoot?.gameObject, (child) => child.SetActive(false));
            }
            var pos = objTransform.position;
            // The ray emission point
            var top = new Vector3(pos.x, this.TopDistance, pos.z);
            var ray = new Ray(top, Vector3.down);
            RaycastHit hitInfo;
            // Detect collision between ray and other colliders
            // ray`s direction is vertically downward
            var ret = Physics.Raycast(ray, out hitInfo);
            if (ret)
            {
                var newPos = hitInfo.point + new Vector3(0, this.Precision, 0);
                objTransform.position = newPos;
            }
            else if (objTransform.localPosition.y != 0)
            {
                ret = true;
                objTransform.localPosition = new Vector3(objTransform.localPosition.x, 0, objTransform.localPosition.z);
            }

            //active objTransform
            objTransform.gameObject.SetActive(true);
            // show all unit
            if (closeUnit)
            {
                XSUnityUtils.ActionChildren(this.unitRoot?.gameObject, (child) => child.SetActive(true));
            }
            return ret;
        }
    }
}
