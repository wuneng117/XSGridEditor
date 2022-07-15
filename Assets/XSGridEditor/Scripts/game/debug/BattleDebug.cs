/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: Demo_1 场景测试寻路
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace XSSLG
{
    /// <summary> Demo_1 场景测试寻路 </summary>
    public class BattleDebug : MonoBehaviour
    {
        /// <summary> 行走速度 </summary>
        public int movementAnimationSpeed = 2;

        /// <summary> tile 管理 </summary>
        public GridMgr GridMgr { get; set; }
        /// <summary> 当前是否在寻路中 </summary>
        public bool IsMoving { get; private set; } = false;
        /// <summary> 行走的对象 </summary>
        public GameObject role = null;
        public PanAndZoom PanObj;

        // Start is called before the first frame update
        void Start()
        {

            this.GridMgr = new GridMgr();
            var gridHelper = Component.FindObjectOfType<XSGridHelper>();
            if (gridHelper)
            {
                this.PanObj.SetConfinerBound(gridHelper.GetBounds());
            }

            var showMgr = new GridShowMgr(this.GridMgr);
            var list = new List<Vector3Int>();
            list.Add(new Vector3Int(0, 0, 0));
            list.Add(new Vector3Int(1, 1, 0));
            showMgr.TestShowRegion(list);
        }

        // Update is called once per frame
        void Update()
        {
            // 左键点击进行寻路
            if (Mouse.current.leftButton.wasPressedThisFrame && !this.IsMoving)
            {
                var tile = XSUG.GetMouseTargetTile();
                Debug.Log("tilePos: " + tile.TilePos);

                var srcTile = this.GridMgr.GetTile(role.transform.position);
                var path = this.GridMgr.FindPath(srcTile, tile);
                this.WalkTo(path);
            }
        }

        /// <summary>
        /// 移动到指定位置
        /// </summary>
        /// <param name="path">移动路径</param>
        public void WalkTo(List<Vector3Int> path)
        {
            if (this.movementAnimationSpeed > 0)
                StartCoroutine(MovementAnimation(path));
        }

        /// <summary> 携程函数处理移动 </summary>
        public virtual IEnumerator MovementAnimation(List<Vector3Int> path)
        {
            this.IsMoving = true;
            path.Reverse(); // 寻路要反一下顺序
            foreach (var pos in path)
            {
                var worldPos = this.GridMgr.GetTile(pos).WorldPos;
                while (this.role.transform.position != worldPos)
                {
                    this.role.transform.position = Vector3.MoveTowards(this.role.transform.position, worldPos, Time.deltaTime * movementAnimationSpeed);
                    yield return 0;
                }
            }
            this.IsMoving = false;
        }
    }
}
