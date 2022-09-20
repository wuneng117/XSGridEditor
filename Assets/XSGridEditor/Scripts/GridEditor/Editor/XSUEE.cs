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
    protected XSUEE() {}

    /// <summary> pop-up reminder </summary>
    public static void ShowTip(string desc)
    {
        var tip = ScriptableObject.CreateInstance<XSPopUpView>();
        tip.Init(300, 100, desc);
        tip.ShowPopup();
    }

    public static XSMain GetMain()
    {
        StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
        return currentStageHandle.FindComponentOfType<XSMain>();
    }
}
