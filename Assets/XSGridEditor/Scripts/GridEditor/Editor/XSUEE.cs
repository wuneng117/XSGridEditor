/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 20:42:18
/// @Description: 
/// </summary>
using UnityEngine;
using XSSLG;

public class XSUEE
{
    /// <summary> 弹窗提醒 </summary>
    public static void ShowTip(string desc)
    {
        var tip = ScriptableObject.CreateInstance<PopUpView>();
        tip.Init(300, 100, desc);
        tip.ShowPopup();
    }
}
