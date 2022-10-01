using UnityEngine;
using XSSLG;
using UnityEngine.InputSystem;

public class XSUG : XSU
{
    protected XSUG() { }

    /// <summary>
    /// Get the tile the mouse is pointing at
    /// </summary>
    /// <param name="camera">The main view camera, if this parameter is not passed in, it will be set to the first found Camera component in the scene</param>
    /// <returns></returns>
    public static XSTile GetMouseTargetTile(Camera camera)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = XSUG.GetMouseHit(screenPos, "Tile", camera);
        var tileData = hit.collider?.gameObject.GetComponent<XSITileNode>();
        if (tileData == null || tileData.IsNull())
        {
            return null;
        }

        var tile = XSInstance.GridMgr.GetXSTileByWorldPos(tileData.WorldPos);
        return tile;
    }

    public static XSTile GetMouseTargetTile() => XSUG.GetMouseTargetTile(XSUG.GetMainCamera());

    /// <summary>
    /// Get the unit the mouse is pointing at
    /// </summary>
    /// <param name="camera">The main view camera, if this parameter is not passed in, it will be set to the first found Camera component in the scene</param>
    /// <returns></returns>
    public static XSIUnitNode GetMouseTargetUnit(Camera camera)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = XSUG.GetMouseHit(screenPos, "Unit", camera);
        var unitData = hit.collider?.gameObject.GetComponent<XSIUnitNode>();
        return unitData;
    }

    public static XSIUnitNode GetMouseTargetUnit() => XSUG.GetMouseTargetUnit(XSUG.GetMainCamera());

    /// <summary>
    /// 摄像机移动到指定位置
    /// </summary>
    /// <param name="worldPos"></param>
    public static void CameraGoto(Vector3 worldPos) => GetBattleNode().XSCamera.MoveTo(worldPos);

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
    /// <summary> UI管理 </summary>
    public static UIMgr GetUIMgr() => XSInstance.UIMgr;

    #endregion


    #region GameScene 获取全局对象
    /// <summary> 获取GameScene对象 </summary>
    public static GameScene GetGameScene() => GameObject.Find(GameConst.COMPONENT_NAME_GAMESCENE)?.GetComponent<GameScene>();
    #endregion


    #region BattleScene 获取全局对象
    /// <summary> 获取BattleLogic对象 </summary>
    public static BattleLogic GetBattleLogic() => XSUG.GetBattleNode()?.Logic;

    /// <summary> 挂场景节点的组件 </summary>
    public static BattleNode GetBattleNode() => GameObject.Find(GameConst.COMPONENT_NAME_BATTLE_INIT)?.GetComponent<BattleNode>();

    #endregion
}
