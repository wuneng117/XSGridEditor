using UnityEngine;
using XSSLG;
using UnityEngine.InputSystem;

public class XSUG : XSUnityUtils
{
    protected XSUG() {}


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
            {
                return camera;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取鼠标所指向的对象
    /// </summary>
    /// <param name="screenPos">屏幕坐标</param>
    /// <param name="layerName">鼠标射线和哪个layer相交，一般和terrian的layer相交</param>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    protected static RaycastHit GetMouseHit(Vector2 screenPos, string layerName, Camera camera)
    {
        if (camera == null)
        {
            return new RaycastHit();
        }

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(screenPos);
        if (layerName == "" || layerName == null)
        {
            Physics.Raycast(ray, out hit);
        }
        else
        {
            var index = LayerMask.NameToLayer(layerName);
            Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << index);
        }

        return hit;
    }

    protected static RaycastHit GetMouseHit(Vector2 screenPos, string layerName) => XSUG.GetMouseHit(screenPos, layerName, XSUG.GetMainCamera());

    public static RaycastHit GetMouseHit(Vector2 screenPos) => XSUG.GetMouseHit(screenPos, "");

    /// <summary>
    /// 获取鼠标所指向的 tile
    /// </summary>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    public static XSTile GetMouseTargetTile(Camera camera)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = XSUG.GetMouseHit(screenPos, "Tile", camera);
        var tileData = hit.collider?.gameObject.GetComponent<XSITileNode>();
        if (tileData == null || tileData.IsNull())
        {
            return XSTile.Default();
        }

        XSInstance.Instance.GridMgr.GetXSTile(tileData.WorldPos, out var tile);
        return tile ?? XSTile.Default();
    }

    public static XSTile GetMouseTargetTile() => XSUG.GetMouseTargetTile(XSUG.GetMainCamera());

    /// <summary>
    /// 获取鼠标所指向的 unit
    /// </summary>
    /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
    /// <returns></returns>
    public static XSIUnitNode GetMouseTargetUnit(Camera camera)
    {
        var screenPos = Pointer.current.position.ReadValue();
        var hit = XSUG.GetMouseHit(screenPos, "Unit", camera);
        var unitData = hit.collider?.gameObject.GetComponent<XSIUnitNode>();
        return unitData;
    }

    public static XSIUnitNode GetMouseTargetUnit() => XSUG.GetMouseTargetUnit(XSUG.GetMainCamera());
}
