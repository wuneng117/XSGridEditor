using TMPro;
using UnityEngine;
using XSSLG;

public class XSUE : UnityUtils
{
    /// <summary>
    /// 生成一个适配size大小的textmeshpro字体节点，暂时用的默认字体
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static TextMeshPro CreateTextMesh(Vector2 size, Transform parent)
    {
        var textNode = new GameObject();
        textNode.name = "TilePosNode";
        var trans = textNode.AddComponent<RectTransform>();
        trans.SetParent(parent.transform);
        trans.sizeDelta = size;

        var text = textNode.AddComponent<TextMeshPro>();
        text.enableAutoSizing = true;
        text.fontSizeMin = 1;
        text.fontSizeMax = 100;
        text.alignment = TextAlignmentOptions.Center;
        return text;
    }

    /// <summary> 获取GridMgr TODO 应该是个单例 </summary>
    /// TODO 让 XSGridHelper 的start方法在编辑器里调用，生成gridmgr，这里用生成的那个；再加个updatemgr方法，在tiel更新时都要调用
    public static GridMgr GetGridMgr() => XSGridHelper.Instance.GridMgr;
}
