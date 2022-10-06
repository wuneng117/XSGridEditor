/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 20:42:18
/// @Description: 
/// </summary>
using UnityEditor.SceneManagement;
using UnityEngine;
using XSSLG;

public class XSUEE
{
    protected XSUEE() { }

    protected static XSGridMainEditMode gridMainEditMode;
    public static XSPrefabNodeMgr PrefabNodeMgr { get => XSUEE.GetGridMainEditMode()?.PrefabNodeMgr; }

    public static XSUnitMgrEditMode UnitMgrEditMode { get => XSUEE.GetGridMainEditMode()?.UnitMgrEditMode; }

    public static XSGridHelperEditMode GridHelperEditMode { get => XSUEE.GetGridMainEditMode()?.GridHelperEditMode; }

    /// <summary> pop-up reminder </summary>
    public static void ShowTip(string desc)
    {
        var tip = ScriptableObject.CreateInstance<XSPopUpView>();
        tip.Init(desc);
        tip.ShowPopup();
    }

    public static XSGridMainEditMode GetGridMainEditMode()
    {
        if (XSUEE.gridMainEditMode == null)
        {

            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            XSUEE.gridMainEditMode = currentStageHandle.FindComponentOfType<XSGridMainEditMode>();
        }
        return XSUEE.gridMainEditMode;
    }
}
