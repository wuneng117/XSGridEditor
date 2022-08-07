using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;
using UnityEngine.InputSystem;

public class XSUG : UnityUtils
{
    /// <summary>
    /// 返回场景中的第一个Camera，名字为SceneCamera是系统的（暂时不清楚什么用），不是场景中的一般camera
    /// </summary>
    /// <returns></returns>
    public static Camera GetMainCamera()
    {
        var cameras = Resources.FindObjectsOfTypeAll<Camera>();
        foreach (var camera in cameras)
        {
            if (camera.name != "SceneCamera")
                return camera;
        }
        return null;
    }

    /// <summary>
    /// 获取鼠标所指向的对象
    /// </summary>
    /// <param name="screenPos">屏幕坐标</param>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <param name="layerName">鼠标射线和哪个layer相交，一般和terrian的layer相交</param>
    /// <returns></returns>
    protected static RaycastHit GetMouseHit(Vector2 screenPos, Camera camera = null, string layerName = null)
    {
        camera = camera ?? XSUG.GetMainCamera();
        if (camera == null)
            return new RaycastHit();

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(screenPos);
        if (layerName != null)
        {
            var index = LayerMask.NameToLayer(layerName);
            Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << index);
        }
        else
            Physics.Raycast(ray, out hit);
        return hit;
    }

    /// <summary>
    /// 获取鼠标所指向的 tile
    /// </summary>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    public static XSTile GetMouseTargetTile(Camera camera = null)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = XSUG.GetMouseHit(screenPos, camera, "Tile");
        var tileData = hit.collider?.gameObject.GetComponent<XSTileData>();
        if (tileData == null)
            return XSTile.Default();

        var tile = XSInstance.Instance.GridMgr.GetXSTile(tileData.transform.position);
        return tile ?? XSTile.Default();
    }

    /// <summary>
    /// 获取鼠标所指向的 unit
    /// </summary>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    public static XSUnitData GetMouseTargetUnit(Camera camera = null)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = XSUG.GetMouseHit(screenPos, camera, "Unit");
        var unitData = hit.collider?.gameObject.GetComponent<XSUnitData>();
        return unitData;
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
        var hit = XSUG.GetMouseHit(screenPos, camera, "Ground");
        return hit.point;
    }
}
