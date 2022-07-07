/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 扩展 unity tilpmap 画格子的功能
/// </summary>
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace XSSLG
{
    /// <summary> 扩展 unity tilpmap 画格子的功能 </summary>
    public class XSGridHelper : MonoBehaviour
    {
        /// <summary> unit根节点 </summary>
        public Transform UnitRoot = null;
        public GameObject ObjectPrefab = null;
        /// <summary> tile 相对于其他物体的抬高高度，防止重叠导致显示问题 </summary>
        public float Precision = 0.01f;

        /// <summary> 射线检测的高度 </summary>
        public float TopDistance = 100f;

        /// <summary> 是否显示移动消耗 </summary>
        public bool IsShowCost = false;

        /// <summary> tile 的prefab 文件 </summary>
        public GameObject TilePrefab = null;

        /// <summary> 移动范围用的图片 </summary>
        public Sprite TileSpriteMove = null;

        /// <summary> 攻击范围用的图片 </summary>
        public Sprite TileSpriteAttack = null;
        /// <summary> 攻击效果范围用的图片 </summary>
        public Sprite TileSpriteAttackEffect = null;

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

        /// <summary> 获取所有 XSTileData 节点 </summary>
        public XSTileData[] GetTileDataArray()
        {
            XSTileData[] ret = { };
            var tileMap = this.GetComponentInChildren<Tilemap>();
            if (tileMap == null) return ret;
            ret = tileMap.gameObject.GetComponentsInChildren<XSTileData>();
            return ret;
        }

        /// <summary>
        /// 调整所有tile的高度
        /// </summary>
        public virtual void SetTileToNearTerrain()
        {
            var tiles = this.GetTileDataArray();
            foreach (var tile in tiles)
                this.SetTileToNearTerrain(tile.gameObject);
        }

        /// <summary>
        /// 每个 tile 根据中心可能有高低不平的障碍物，调整 tile 的高度到障碍物的顶端
        /// 比如从 tile 中心点抬高100，再检测100以内有没有碰撞物，碰到的话就把 tile 的高度设置到碰撞点的位置
        /// </summary>
        public virtual void SetTileToNearTerrain(GameObject tile)
        {
            // 隐藏tile ，防止射线碰到 tile
            tile.SetActive(false);
            var pos = tile.transform.position;
            // 射线发射点，抬高 tile 以后的中心点
            var top = new Vector3(pos.x, TopDistance, pos.z);
            var ray = new Ray(top, Vector3.down);
            RaycastHit hitInfo;
            // 这里检测 ray 和其他物体的碰撞
            // ray 位置和射线方向，位置是 抬高TopDistance后的坐标，方向垂直向下
            if (!Physics.Raycast(ray, out hitInfo))
                tile.transform.position = pos;  // 把 tile 放到原来的位置
            else
                tile.transform.position = hitInfo.point + new Vector3(0, Precision, 0);
            //激活tile
            tile.SetActive(true);
        }

        /// <summary> 删除所有的 tile </summary>
        public virtual void ClearTiles()
        {
            var tileMap = this.GetComponentInChildren<Tilemap>();
            XSU.RemoveChildren(tileMap.gameObject);
        }

        /// <summary>
        /// 显示 tilepos
        /// </summary>
        /// <param name="isShow">是否显示</param>
        public virtual void SetTilePosShow(bool isShow)
        {
            this.SetTextShow(isShow,
                            XSGridDefine.GAMEOBJECT_TILE_POS_ROOT,
                            (quad, text) => text.text = string.Format("{0},{1}", Math.Floor(quad.transform.position.x), Math.Floor(quad.transform.position.z))
            );
        }

        /// <summary>
        /// 显示 tile cost
        /// </summary>
        /// <param name="isShow">是否显示</param>
        public virtual void SetTileCostShow(bool isShow)
        {
            this.SetTextShow(isShow,
                            XSGridDefine.GAMEOBJECT_TILE_COST_ROOT,
                            (quad, text) =>
                            {
                                text.text = quad.Cost.ToString();
                                if (quad.Cost <= XSGridDefine.TILE_COST_COLOR.Length - 1)
                                    text.color = XSGridDefine.TILE_COST_COLOR[quad.Cost];
                                else
                                    text.color = Color.red;
                            });
        }

        /// <summary>
        /// 为所有 tile 生成需要显示的文字
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <param name="rootName">所有文字的根节点</param>
        /// <param name="afterCreateFn">生成后的回调</param>
        protected virtual void SetTextShow(bool isShow, string rootName, Action<XSTileData, TextMeshPro> afterCreateFn)
        {
            if (isShow)
            {
                var textRoot = new GameObject();
                textRoot.name = rootName;
                var tiles = this.GetTileDataArray();
                foreach (var tile in tiles)
                {
                    var parTrans = tile.transform;
                    var size = new Vector2(parTrans.localScale.x * 0.8f, parTrans.localScale.z * 0.85f);
                    var text = XSU.CreateTextMesh(size, textRoot.transform);
                    text.transform.position = tile.transform.position;
                    text.transform.Rotate(new Vector3(90, 0, 0));
                    afterCreateFn(tile, text);
                }
            }
            else
                DestroyImmediate(GameObject.Find(rootName));
        }

        /// <summary> 创建一个XSObject </summary>
        public void CreateObject()
        {
            if (XSU.IsEditor())
            {
                GameObject ret;
                do
                {
                    Transform parent = this.GetUnitRoot();
                    if (parent == null)
                        break;

                    // 从prefab创建引用的gameobject
                    ret = PrefabUtility.InstantiatePrefab(this.ObjectPrefab, parent) as GameObject;
                    if (ret == null)
                        break;

                    // 就是查找显示中的网格中第一个，然后把生成的prefab放到哪个网格的位置
                    var defaultGrid = GameObject.Find("Grid/Tilemap")?.transform.GetChild(0);
                    if (defaultGrid == null)
                        break;

                    ret.transform.position = defaultGrid.position;
                } while (false);
            }
        }

        virtual protected Transform GetUnitRoot()
        {
            var parentName = "Grid/Unit";
            var parent = GameObject.Find(parentName)?.transform;
            return parent;
        }

        // /// <summary> 所有 XSObject 的坐标对齐所在 tile 中心 </summary>
        // public void SetObjectToTileCenter()
        // {
        //     Transform parent = this.GetUnitRoot();
        //     if (parent == null)
        //         return;

        //     var gridMgr = new GridMgr();
        //     foreach (Transform child in parent)
        //         child.position = gridMgr.WorldToTileCenterWorld(child.position);
        // }

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