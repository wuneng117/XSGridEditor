/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: Demo_1 场景测试
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XSSLG
{
    /// <summary> Demo_1 场景测试 </summary>
    public class XSBattleMgr : MonoBehaviour
    {
        /// <summary> 行走速度 </summary>
        protected int movementAnimationSpeed = 2;

        protected XSCamera camera;

        /// <summary> tile 管理 </summary>
        public XSIGridMgr GridMgr { get; set; }

        /// <summary> unit 管理 </summary>
        public XSUnitMgr UnitMgr { get; protected set; }

        /// <summary> 格子显示管理 </summary>
        public XSGridShowMgr GridShowMgr { get; set; }

        /// <summary> 当前是否在移动中 </summary>
        public bool IsMoving { get; private set; };
        

        public List<Vector3> MoveRegion { get; private set; }

        protected XSUnitNode SelectedUnit { get; set; };


        void Start()
        {
            if (XSUnityUtils.IsEditor())
            {
                this.enabled = false;
            }
            else
            {
                this.GridMgr = XSInstance.Instance.GridMgr;
                var gridHelper = XSInstance.Instance.GridHelper;
                if (gridHelper)
                {
                    this.camera.SetConfinerBound(gridHelper.GetBounds());
                }

                this.UnitMgr = new XSUnitMgr(gridHelper);

                var moveRegionCpt = XSGridShowRegionCpt.Create(XSGridDefine.SCENE_GRID_MOVE, gridHelper.MoveTilePrefab, 10);
                this.GridShowMgr = new XSGridShowMgr(moveRegionCpt);
            }
        }

        // Update is called once per frame
        void Update()
        {
            /************************* 处理选择 unit ，然后移动 begin ***********************/
            if (!this.IsMoving)
            {
                // 左键点击进行寻路
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (this.SelectedUnit)
                    {
                        var tile = XSUG.GetMouseTargetTile();
                        Debug.Log("tilePos: " + tile.TilePos);

                        // 要在移动范围内的格子
                        if (this.MoveRegion.Contains(tile.WorldPos))
                        {
                            this.GridShowMgr.ClearMoveRegion();
                            this.MoveRegion = null;
                            //缓存
                            if (this.SelectedUnit.CachedPaths != null && this.SelectedUnit.CachedPaths.ContainsKey(tile.WorldPos))
                            {
                                this.WalkTo(this.SelectedUnit.CachedPaths[tile.WorldPos]);
                            }
                            else
                            {
                                this.SelectedUnit = null;
                            }
                        }
                    }
                    else
                    {
                        var unit = (XSUnitNode)XSUG.GetMouseTargetUnit();
                        if (unit != null)
                        {
                            Debug.Log("SelectedUnit: " + unit.name);
                            this.MoveRegion = this.GridShowMgr.ShowMoveRegion(unit);
                            this.SelectedUnit = unit;
                        }

                    }
                    /************************* 处理选择 unit ，然后移动  end  ***********************/
                }
                else if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    if (this.SelectedUnit)
                    {
                        this.GridShowMgr.ClearMoveRegion();
                        this.SelectedUnit = null;
                    }
                }
            }
        }

        /// <summary>
        /// 移动到指定位置
        /// </summary>
        /// <param name="path">移动路径</param>
        public void WalkTo(List<Vector3> path)
        {
            if (this.movementAnimationSpeed > 0)
            {
                StartCoroutine(MovementAnimation(path));
            }
        }

        /// <summary> 携程函数处理移动 </summary>
        public virtual IEnumerator MovementAnimation(List<Vector3> path)
        {
            this.IsMoving = true;
            path.Reverse(); // 寻路要反一下顺序
            foreach (var pos in path)
            {
                while (this.SelectedUnit.transform.position != pos)
                {
                    this.SelectedUnit.transform.position = Vector3.MoveTowards(this.SelectedUnit.transform.position, pos, Time.deltaTime * movementAnimationSpeed);
                    yield return 0;
                }
            }
            this.SelectedUnit = null;
            this.IsMoving = false;
        }
    }
}
