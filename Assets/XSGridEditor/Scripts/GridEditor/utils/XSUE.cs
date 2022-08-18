using TMPro;
using UnityEngine;
using XSSLG;

public class XSUE : XSUnityUtils
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
}
