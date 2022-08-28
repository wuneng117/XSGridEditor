/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 画格子的功能
/// </summary>
using System;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 扩展 unity tilpmap 画格子的功能 </summary>
    [RequireComponent(typeof(XSGridHelper))]
    [ExecuteInEditMode]
    public class XSGridHelperEditMode : MonoBehaviour
    {
        [SerializeField]
        protected GameObject UniytPrefab;
        /// <summary> tile 相对于其他物体的抬高高度，防止重叠导致显示问题 </summary>
        protected float Precision = 0.01f;

        /// <summary> 射线检测的高度 </summary>
        protected float TopDistance = 100f;

        /// <summary> 监控CellSize变化 </summary>
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


        #region  Tile 操作

        /// <summary> 是否显示 tilepos </summary>
        public bool IsShowTilePos
        {
            get => GameObject.Find(XSGridDefine.GAMEOBJECT_TILE_POS_ROOT);
        }

        /// <summary> 是否显示 tilecost </summary>
        public bool IsShowTileCost
        {
            get => GameObject.Find(XSGridDefine.GAMEOBJECT_TILE_COST_ROOT);
        }

        /// <summary>
        /// 添加XSTile
        /// </summary>
        /// <param name="tileData"></param>
        /// <returns></returns>
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

            this.SetTileToNearTerrain(tile);
            return tile;
        }

        /// <summary>
        /// 删除XSTile
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
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
        /// 调整所有tile的高度
        /// </summary>
        public virtual void SetTileToNearTerrain()
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            foreach (var tile in XSInstance.Instance.GridMgr.GetAllTiles())
            {
                this.SetTileToNearTerrain(tile);
            }
        }

        /// <summary>
        /// 每个 tile 根据中心可能有高低不平的障碍物，调整 tile 的高度到障碍物的顶端
        /// 比如从 tile 中心点抬高100，再检测100以内有没有碰撞物，碰到的话就把 tile 的高度设置到碰撞点的位置
        /// </summary>
        protected virtual bool SetTileToNearTerrain(XSTile tile)
        {
            var ret = false;
            if (tile.Node == null || tile.Node.IsNull())
            {
                return ret;
            }

            var node = tile.Node;
            ret = this.SetTileToNearTerrain(((XSTileNode)node).transform);
            if (!ret)
            {
                return ret;
            }

            // 调整了位置，需要更新XSTile和XSTileNodeEditMode的位置
            tile.WorldPos = node.WorldPos;
            return ret;
        }

        /// <summary>
        /// 每个 tile 根据中心可能有高低不平的障碍物，调整 tile 的高度到障碍物的顶端
        /// 比如从 tile 中心点抬高100，再检测100以内有没有碰撞物，碰到的话就把 tile 的高度设置到碰撞点的位置
        /// </summary>
        protected virtual bool SetTileToNearTerrain(Transform tileData)
        {
            // 隐藏tile ，防止射线碰到 tile
            tileData.gameObject.SetActive(false);
            // 隐藏所有unit，防止参与射线检测
            XSUnityUtils.ActionChildren(this.GetUnitRoot()?.gameObject, (child) => child.SetActive(false));
            var pos = tileData.position;
            // 射线发射点，抬高 tile 以后的中心点
            var top = new Vector3(pos.x, TopDistance, pos.z);
            var ray = new Ray(top, Vector3.down);
            RaycastHit hitInfo;
            // 这里检测 ray 和其他物体的碰撞
            // ray 位置和射线方向，位置是 抬高TopDistance后的坐标，方向垂直向下
            var ret = Physics.Raycast(ray, out hitInfo);
            if (ret)
            {
                var newPos = hitInfo.point + new Vector3(0, Precision, 0);
                tileData.position = newPos;
            }
            // 如果贴不到任何东西，那就设置localpos的高度y为0
            else if (tileData.localPosition.y != 0)
            {
                ret = true;
                tileData.localPosition = new Vector3(tileData.localPosition.x, 0, tileData.localPosition.z);
            }

            //激活tile
            tileData.gameObject.SetActive(true);
            // 显示所有unit，防止参与射线检测
            XSUnityUtils.ActionChildren(this.GetUnitRoot()?.gameObject, (child) => child.SetActive(true));
            return ret;
        }

        public virtual void SetPrefabToNearTerrain(Transform trans)
        {
            this.SetTileToNearTerrain(trans);
        }

        /// <summary> 删除所有的 tile </summary>
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
        /// 显示 tilepos
        /// </summary>
        /// <param name="isShow">是否显示</param>
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
        /// 显示 tile cost
        /// </summary>
        /// <param name="isShow">是否显示</param>
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
        /// 为所有 tile 生成需要显示的文字
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <param name="rootName">所有文字的根节点</param>
        /// <param name="afterCreateFn">生成后的回调</param>
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
                    text.transform.position = parTrans.position;
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

        #region Unit 操作
        /// <summary> 创建一个XSObject </summary>
        public virtual void CreateObject()
        {
            if (!XSUE.IsEditor())
            {
                return;
            }

            Transform parent = this.GetUnitRoot();
            if (parent == null)
            {
                return;
            }

            // 从prefab创建引用的gameobject
            var ret = PrefabUtility.InstantiatePrefab(this.UniytPrefab, parent) as GameObject;
            if (ret == null)
            {
                return;
            }

            // 就是查找显示中的网格中第一个，然后把生成的prefab放到哪个网格的位置
            var defaultGrid = this.GetTileRoot()?.transform.GetChild(0);
            if (defaultGrid == null)
            {
                return;
            }

            ret.transform.position = defaultGrid.position;
            ret.layer = LayerMask.NameToLayer(XSGridDefine.LAYER_UNIT);
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