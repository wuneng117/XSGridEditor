using UnityEngine;
using UnityEngine.InputSystem;

namespace XSSLG
{
    public partial class XSU
    {

        /// <summary>
        /// Get the tile the mouse is pointing at
        /// </summary>
        /// <param name="camera">The main view camera, if this parameter is not passed in, it will be set to the first found Camera component in the scene</param>
        /// <returns></returns>
        public static XSTile GetMouseTargetTile(Camera camera)
        {
            var screenPos = Pointer.current.position.ReadValue();
            var hit = XSU.GetMouseHit(screenPos, "Tile", camera);
            var tileData = hit.collider?.gameObject.GetComponent<XSITileNode>();
            if (tileData == null || tileData.IsNull())
            {
                return null;
            }

            var tile = XSU.GridMgr.GetXSTileByWorldPos(tileData.WorldPos);
            return tile;
        }

        public static XSTile GetMouseTargetTile() => XSU.GetMouseTargetTile(XSU.GetMainCamera());

        /// <summary>
        /// Get the unit the mouse is pointing at
        /// </summary>
        /// <param name="camera">The main view camera, if this parameter is not passed in, it will be set to the first found Camera component in the scene</param>
        /// <returns></returns>
        public static XSIUnitNode GetMouseTargetUnit(Camera camera)
        {
            var screenPos = Pointer.current.position.ReadValue();
            var hit = XSU.GetMouseHit(screenPos, "Unit", camera);
            var unitData = hit.collider?.gameObject.GetComponent<XSIUnitNode>();
            return unitData;
        }

        public static XSIUnitNode GetMouseTargetUnit() => XSU.GetMouseTargetUnit(XSU.GetMainCamera());

        /// <summary>
        /// 摄像机以一定速度移动到指定位置
        /// </summary>
        /// <param name="worldPos"></param>
        public static void CameraMoveTo(Vector3 worldPos) => GetBattleNode().XSCamera.MoveTo(worldPos);

        /// <summary>
        /// 摄像机移动到指定位置
        /// </summary>
        /// <param name="worldPos"></param>
        public static void CameraSetPosTo(Vector3 worldPos) => GetBattleNode().XSCamera.SetPosTo(worldPos);

        /// <summary>
        /// 摄像机移动到指定位置
        /// </summary>
        /// <param name="worldPos"></param>
        public static void CameraCanFreeMove(bool val) => GetBattleNode().XSCamera.CanFreeMove = val;

        // TODO
        public static void ShowDamageText(string damage, Transform transform)
        {
            // var mgr = XSU.GetBattleNode()?.TextDamageManager;
            // if (mgr == null)
            //     return;

            // mgr.Add(damage, transform, "default");
        }

        #region 获取全局对象

        /// <summary> 获取GameScene对象 </summary>
        public static GameScene GetGameScene() => GameObject.Find(GameConst.COMPONENT_NAME_GAMESCENE)?.GetComponent<GameScene>();

        /// <summary> 获取BattleLogic对象 </summary>
        public static BattleLogic GetBattleLogic() => XSU.GetBattleNode()?.Logic;

        /// <summary> 挂场景节点的组件 </summary>
        public static BattleNode GetBattleNode() => GameObject.Find(GameConst.COMPONENT_NAME_BATTLE_INIT)?.GetComponent<BattleNode>();

        /// <summary> UI管理 </summary>
        public static UIMgr UIMgr { get => XSU.GetBattleNode().UIMgr; }

        #endregion
    }
}
