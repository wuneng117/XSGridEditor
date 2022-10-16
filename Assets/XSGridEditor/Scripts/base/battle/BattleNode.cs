/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 负责战斗初始化
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XSSLG;
using System;
using System.Linq;
// using Guirao.UltimateTextDamage;

namespace XSSLG
{

    [DefaultExecutionOrder(Config.ExecutionOrder.BATTLE_INIT)]
    public class BattleNode : MonoBehaviour
    {
        /************************* UI begin ***********************/
        public UIMgr UIMgr { get; protected set; } = new UIMgr();

        /// <summary> 所有ui的根节点 </summary>
        public GameObject UI;

        /// <summary> 起始关闭界面的ui根节点 </summary>
        public GameObject CloseRoot;

        [SerializeField]
        /// <summary> unit行动菜单 </summary>
        private UnitMenu unitMenu;

        [SerializeField]
        /// <summary> unit详情 </summary>
        private RolePanel rolePanel;

        [SerializeField]
        /// <summary> 主菜单 </summary>
        private MainMenu mainMenu;

        // /// <summary> 光标跟随鼠标移动 </summary>
        // public BattleMouseCuror mouseCuror;

        [SerializeField]
        /// <summary> 敌人回合开始 </summary>
        private GameObject enemyTurn;

        [SerializeField]
        /// <summary> 玩家回合开始 </summary>
        private GameObject playerTurn;

        /// <summary> 左下角玩家信息 </summary>
        public UnitInfoTip unitInfoTip;

        /************************* UI  end  ***********************/

        /// <summary> 摄像机 </summary>
        public Camera mainCamera;

        [SerializeField]
        protected XSCamera xsCamera;
        public XSCamera XSCamera { get => xsCamera; protected set => xsCamera = value; }

        // /// <summary> 飘字生成 </summary>
        // public UltimateTextDamageManager TextDamageManager;

        /// <summary> 战斗管理 </summary>
        public BattleLogic Logic { get; private set; }

        /// <summary> 网格显示管理 </summary>
        public XSGridShowMgr GridShowMgr { get; set; }

        private void Awake()
        {
            var gridHelper = XSU.GridHelper;
            this.Logic = new BattleLogic();
            var bounds = gridHelper.GetBounds();
            XSU.CameraSetPosTo(bounds.center);

            var moveRegionCpt = XSGridShowRegionCpt.Create(XSGridDefine.SCENE_GRID_MOVE, gridHelper.MoveTilePrefab, 10);
            var attackRegionCpt = XSGridShowRegionCpt.Create(XSGridDefine.SCENE_GRID_MOVE, gridHelper.AttackTilePrefab, 11);
            var attackEffectRegionCpt = XSGridShowRegionCpt.Create(XSGridDefine.SCENE_GRID_MOVE, gridHelper.AttackEffectTilePrefab, 12);
            this.GridShowMgr = new XSGridShowMgr(moveRegionCpt, attackRegionCpt, attackEffectRegionCpt);

            this.InitUI();
        }

        /// <summary> 初始化ui </summary>
        private void InitUI()
        {
            this.UI?.SetActive(true);
            foreach (Transform child in this.CloseRoot.transform)
                child.gameObject.SetActive(false);
            this.playerTurn.SetActive(false);
            this.enemyTurn.SetActive(false);
        }

        // Start is called before the first frame update
        private void Start()
        {
            var bounds = XSU.GridHelper.GetBounds();
            this.xsCamera.SetConfinerBound(bounds);
            this.Logic.Change(new PhaseGameStart());
        }

        // Update is called once per frame
        void Update()
        {
            Logic?.Update();
        }
        /************************* ui操作 begin ***********************/
        //TODO 
        /// <summary> 打开关闭unit行动菜单 </summary>
        public void OpenRoleMenu(Vector3 screenPos)
        {
            XSU.UIMgr.ShowUI(this.unitMenu?.gameObject);
            if (this.unitMenu != null)
            {
                this.unitMenu.transform.position = screenPos;
            }
        }

        public void OpenRolePanel(UnitBase unit)
        {
            XSU.UIMgr.ShowUI(this.rolePanel?.gameObject);
            this.rolePanel?.init(unit);
        }

        public void CloseRolePanel() => XSU.UIMgr.CloseUITo(this.rolePanel?.gameObject);

        public void CloseRoleMenu() => XSU.UIMgr.CloseUITo(this.unitMenu?.gameObject);

        /// <summary> 打开关闭主菜单 </summary>
        public void OpenMainMenu()
        {
            XSU.CameraCanFreeMove(false);
            XSU.UIMgr.ShowUI(this.mainMenu?.gameObject);
        }
        public void CloseMainMenu()
        {
            XSU.CameraCanFreeMove(true);
            XSU.UIMgr.CloseUITo(this.mainMenu?.gameObject);
        }

        /// <summary> 打开关闭回合显示 </summary>
        public void OpenTurnChange(GroupType type) => this.SetTurnChange(type, true);

        public void CloseTurnChange(GroupType type) => this.SetTurnChange(type, false);

        private void SetTurnChange(GroupType type, bool val)
        {
            if (type == GroupType.Self)
                this.playerTurn?.SetActive(val);
            else if (type == GroupType.Enemy)
                this.enemyTurn?.SetActive(val);
        }

        public void UpdateUnitInfoTip(XSTile mouseTile)
        {
            if (mouseTile == null)
            {
                XSU.GetBattleNode().unitInfoTip.Close();
                return;
            }

            var unit = this.Logic.UnitMgr.GetUnitByCellPosition(mouseTile.TilePos);
            if (unit == null)
            {
                XSU.GetBattleNode().unitInfoTip.Close();
                return;
            }

            XSU.GetBattleNode().unitInfoTip.Open(unit);
        }

        /************************* ui操作  end  ***********************/
    }
}
