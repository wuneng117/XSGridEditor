/// <summary>
/// @Author: zhoutao
/// @Date: 2021/529
/// @Description: 战斗地图里，随着鼠标的移动，在对应网格上显示光标
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;
namespace XSSLG
{
    public class BattleMouseCuror : MonoBehaviour
    {
        /// <summary> 鼠标图片 </summary>
        public SpriteRenderer mouseCuror;

        private XSIGridMgr GridMgr { get; set; }

        /// <summary> 是否跟随移动 </summary>
        public bool Active { get; set; } = true;
        // Start is called before the first frame update
        void Start()
        {
            this.GridMgr = XSU.GridMgr;
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.Active)
                return;

            this.SetWorldPosition(XSU.GetMouseTargetTile());

            // TODO
            // if（this.Phase is PhaseChooseMove)
            //     m_MainScene->getActionPlayer()->setDuration(m_grid);
            // }
        }

        /// <summary> 设置下光标的世界坐标 </summary>
        private void SetWorldPosition(XSTile tile) 
        {
            if (tile == null)
            {
                // 如果到了外面,应该隐藏这个curor,直接屏幕显示鼠标吧
                return;
            }
            
            var worldPos = tile .WorldPos + new Vector3(0, 0.12f, 0);   // 地面有抬高一点的
            this.mouseCuror.transform.position = worldPos;//控制物体移动
        }
    }
}
