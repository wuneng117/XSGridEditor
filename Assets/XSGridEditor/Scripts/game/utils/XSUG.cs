using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;
using UnityEngine.InputSystem;

public class XSUG : UnityUtils
{
    #region 获取全局对象
    /// <summary> UI管理 </summary>
    // public static UIMgr GetUIMgr() => GameObject.Find(GameConst.COMPONENT_NAME_MAIN)?.GetComponent<main>()?.UIMgr;

    // #endregion


    // #region GameScene 获取全局对象
    // /// <summary> 获取GameScene对象 </summary>
    // public static GameScene GetGameScene() => GameObject.Find(GameConst.COMPONENT_NAME_GAMESCENE)?.GetComponent<GameScene>();
    // #endregion


    // #region BattleScene 获取全局对象
    // /// <summary> 获取BattleLogic对象 </summary>
    // public static BattleLogic GetBattleLogic() => XSU.GetBattleNode()?.Logic;

    // /// <summary> 挂场景节点的组件 </summary>
    // public static BattleNode GetBattleNode() => GameObject.Find(GameConst.COMPONENT_NAME_BATTLE_INIT)?.GetComponent<BattleNode>();

    // /// <summary> 获取GridMgr TODO 应该是个单例 </summary>
    // /// TODO 让 XSGridHelper 的start方法在编辑器里调用，生成gridmgr，这里用生成的那个；再加个updatemgr方法，在tiel更新时都要调用
    // public static GridMgr GetGridMgr() => XSUG.GetBattleLogic().GridMgr;

    #endregion

    /// <summary>
    /// 获取鼠标所指向的 tile
    /// </summary>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    public static XSTile GetMouseTargetTile(Camera camera = null)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = UnityUtils.GetMouseHit(screenPos, camera, "Tile");
        var tile = hit.collider?.gameObject.GetComponent<XSTileData>()?.Tile;
        if (tile == null)
            return XSTile.Default();
        return tile;
    }

    /// <summary> 获取鼠标所在的世界坐标 </summary>
    public static Vector3 GetMousePosition()
    {
        // XSU.Log(this.GetBattleNode()?.mainCamera);
        var screenPos = Pointer.current.position.ReadValue();
        return XSUG.ScreenPosToWorldPos(screenPos);
    }

    /// <summary> 
    /// 获取鼠标所在的世界坐标 
    /// </summary>
    /// <param name="screenPos">屏幕坐标</param>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    public static Vector3 ScreenPosToWorldPos(Vector2 screenPos, Camera camera = null)
    {
        var hit = UnityUtils.GetMouseHit(screenPos, camera, "Ground");
        return hit.point;
    }

    // /// <summary>
    // /// 摄像机移动到指定位置
    // /// </summary>
    // /// <param name="worldPos"></param>
    // public static void CameraGoto(Vector3 worldPos) => GetBattleNode().PanAndMoveObj.MoveTo(worldPos);
    // /// <summary>
    // /// 摄像机移动到指定位置
    // /// </summary>
    // /// <param name="worldPos"></param>
    // public static void CameraCanFreeMove(bool val) => GetBattleNode().PanAndMoveObj.CanFreeMove = val;

    // public static void ShowDamageText(string damage, Transform transform)
    // {
    //     var mgr = XSU.GetBattleNode()?.TextDamageManager;
    //     if (mgr == null)
    //         return;

    //     mgr.Add(damage, transform, "default");
    // }
}
