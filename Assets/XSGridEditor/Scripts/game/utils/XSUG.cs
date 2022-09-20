using UnityEngine;
using XSSLG;
using UnityEngine.InputSystem;

public class XSUG : XSUnityUtils
{
    protected XSUG() {}


    /// <summary>
    /// Returns the first Camera in the scene, the name is SceneCamera is the system (it is not clear what it is used for), not the general camera in the scene
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
    /// Get the object the mouse is pointing at
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="layerName">Which layer the mouse ray intersects with</param>
    /// <param name="camera">The main view camera, if this parameter is not passed in, it will be set to the first found Camera component in the scene</param>
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
            return XSTile.Default();
        }

        XSInstance.Instance.GridMgr.GetXSTile(tileData.WorldPos, out var tile);
        return tile ?? XSTile.Default();
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
}
