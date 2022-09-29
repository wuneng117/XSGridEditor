/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: script added to GridEditor gameobject, to edit grid
/// </summary>
using System;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    /// <summary> the function to draw tile </summary>
    [RequireComponent(typeof(XSGridHelper))]
    [ExecuteInEditMode]
    public class XSGridHelperEditMode : MonoBehaviour
    {
        /// <summary> Monitor CellSize changes </summary>
        private Grid Grid { get; set; }

        protected Vector3 PrevTileSize { get; set; } = Vector3.zero;

        public virtual void Start()
        {
            if (!XSUE.IsEditor())
            {
                this.enabled = false;
            }
            else
            {
                this.Grid = this.GetTileRoot()?.GetComponent<Grid>();
                if (this.Grid)
                {
                    this.PrevTileSize = this.Grid.cellSize;
                }
            }
        }

        protected virtual Transform GetUnitRoot() => XSInstance.Instance.GridHelper.UnitRoot;

        protected virtual Transform GetTileRoot() => XSInstance.Instance.GridHelper.TileRoot;


        #region  Tile function

        public bool IsShowTilePos
        {
            get => GameObject.Find(XSGridDefine.GAMEOBJECT_TILE_POS_ROOT);
        }

        public bool IsShowTileCost
        {
            get => GameObject.Find(XSGridDefine.GAMEOBJECT_TILE_COST_ROOT);
        }

        public virtual XSTile AddXSTile(XSITileNode tileData)
        {
            if (!XSUE.IsEditor())
            {
                return null;
            }

            if (tileData == null || tileData.IsNull())
            {
                return null;
            }

            var mgr = XSInstance.Instance.GridMgr;
            var tile = mgr.AddXSTile(tileData);
            if (tile == null)
            {
                return null;
            }

            this.SetTileToNearTerrain(tile, true);
            return tile;
        }

        public virtual bool RemoveXSTile(Vector3 worldPos)
        {
            if (!XSUE.IsEditor())
            {
                return false;
            }

            var mgr = XSInstance.Instance.GridMgr;
            return mgr.RemoveXSTile(worldPos);
        }

        /// <summary>
        /// Adjust the height of all tiles
        /// </summary>
        public virtual void SetTileToNearTerrain()
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            XSUnityUtils.ActionChildren(this.GetUnitRoot()?.gameObject, (child) => child.SetActive(false));
            foreach (var tile in XSInstance.Instance.GridMgr.GetAllTiles())
            {
                this.SetTileToNearTerrain(tile, false);
            }
            XSUnityUtils.ActionChildren(this.GetUnitRoot()?.gameObject, (child) => child.SetActive(true));
        }

        /// <summary>
        /// Each tile may have uneven obstacles in the center, adjust the height of the tile to the top of the obstacle
        /// For example, raise 100 from the center point of the tile, and then check whether there is 
        /// a collision within 100. If it hits, set the height of the tile to the position of the collision point.
        /// </summary>
        protected virtual bool SetTileToNearTerrain(XSTile tile, bool closeUnit)
        {
            var ret = false;
            if (tile.Node == null || tile.Node.IsNull())
            {
                return ret;
            }

            var node = tile.Node;
            ret = XSInstance.Instance.GridHelper.SetTransToTopTerrain(((XSTileNode)node).transform, closeUnit);
            if (!ret)
            {
                return ret;
            }

            // After adjusting the position, you need to update the position of XSTile and XSTileNodeEditMode
            tile.WorldPos = node.WorldPos;
            return ret;
        }

        public virtual void ClearTiles()
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            XSUE.RemoveChildren(this.GetTileRoot()?.gameObject);
            XSInstance.Instance.GridMgr.ClearAllTiles();
        }

        /// <summary>
        /// show tile position text
        /// </summary>
        /// <param name="isShow"></param>
        public virtual void SetTilePosShow(bool isShow)
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            this.SetTextShow(isShow,
                            XSGridDefine.GAMEOBJECT_TILE_POS_ROOT,
                            (tile, text) => text.text = string.Format("{0},{1}", tile.TilePos.x, tile.TilePos.z)
            );
        }

        /// <summary>
        /// show tile cost text
        /// </summary>
        /// <param name="isShow"></param>
        public virtual void SetTileCostShow(bool isShow)
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            this.SetTextShow(isShow,
                            XSGridDefine.GAMEOBJECT_TILE_COST_ROOT,
                            (tile, text) =>
                            {
                                text.text = tile.Cost.ToString();
                                if (tile.Cost <= XSGridDefine.TILE_COST_COLOR.Length - 1)
                                {
                                    text.color = XSGridDefine.TILE_COST_COLOR[tile.Cost];
                                }
                                else
                                {
                                    text.color = Color.red;
                                }
                            });
        }

        /// <summary>
        /// Generate text to display for all tiles
        /// </summary>
        /// <param name="isShow"></param>
        /// <param name="rootName"></param>
        /// <param name="afterCreateFn"> Callback after generation </param>
        protected virtual void SetTextShow(bool isShow, string rootName, Action<XSTile, TextMeshPro> afterCreateFn)
        {
            if (isShow)
            {
                var textRoot = new GameObject();
                textRoot.name = rootName;
                foreach (var tile in XSInstance.Instance.GridMgr.GetAllTiles())
                {
                    if (tile.Node == null || tile.Node.IsNull())
                    {
                        continue;
                    }

                    var node = (XSTileNode)tile.Node;
                    var parTrans = node.transform;
                    var size = new Vector2(parTrans.localScale.x * 0.8f, parTrans.localScale.z * 0.85f);
                    var text = XSUE.CreateTextMesh(size, textRoot.transform);
                    text.transform.position = new Vector3(parTrans.position.x, parTrans.position.y + 0.01f, parTrans.position.z);
                    text.transform.Rotate(new Vector3(90, 0, 0));
                    afterCreateFn(tile, text);
                }
            }
            else
            {
                DestroyImmediate(GameObject.Find(rootName));
            }
        }

        #endregion

        public virtual void Update()
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            if (this.Grid && this.Grid.cellSize != this.PrevTileSize)
            {
                this.PrevTileSize = this.Grid.cellSize;

                XSInstance.Instance.GridMgr?.UpdateTileSize(this.PrevTileSize);

                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                var main = currentStageHandle.FindComponentOfType<XSMain>();
                main.UnitMgr?.UpdateUnitPos();
            }
        }
    }
}