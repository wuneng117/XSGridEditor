/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: Demo_1 场景测试寻路
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XSSLG
{
    /// <summary> Demo_1 场景测试寻路 </summary>
    public class BattleMgr : MonoBehaviour
    {
        /// <summary> 行走速度 </summary>
        public int movementAnimationSpeed = 2;

        /// <summary> tile 管理 </summary>
        public GridMgr GridMgr { get; set; }

        /// <summary> unit 管理 </summary>
        public XSUnitMgr UnitMgr { get; set; }

        /// <summary> 格子显示管理 </summary>
        public GridShowMgr GridShowMgr { get; set; }

        /// <summary> 当前是否在寻路中 </summary>
        public bool IsMoving { get; private set; } = false;
        
        public PanAndZoom PanObj;

        public List<Vector3> MoveRegion { get; private set; }

        protected XSUnitData SelectedUnit { get; set; } = null;



        // Start is called before the first frame update
        void Start()
        {
            if (UnityUtils.IsEditor())
            {
                this.enabled = false;
            }
            else
            {
                this.GridMgr = XSInstance.Instance.GridMgr;
                var gridHelper = XSInstance.Instance.GridHelper;
                if (gridHelper)
                    this.PanObj.SetConfinerBound(gridHelper.GetBounds());

                this.UnitMgr = new XSUnitMgr(gridHelper);
                this.GridShowMgr = new GridShowMgr(this.GridMgr, gridHelper);
            }


            // var showMgr = new GridShowMgr(this.GridMgr);
            // var list = new List<Vector3Int>();
            // list.Add(new Vector3Int(0, 0, 0));
            // list.Add(new Vector3Int(1, 1, 0));
            // showMgr.TestShowRegion(list);
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
                                this.WalkTo(this.SelectedUnit.CachedPaths[tile.WorldPos]);
                            else
                                this.SelectedUnit = null;
                        }
                    }
                    else
                    {
                        var unit = XSUG.GetMouseTargetUnit();
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
                StartCoroutine(MovementAnimation(path));
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
