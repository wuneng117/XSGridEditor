using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using XSSLG;

public class XSUE : XSUnityUtils
{
    protected XSUE() {}


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

    /// <summary>
    /// 加载文件夹下的内容
    /// </summary>
    /// <param name="pathArray">加载文件夹路径</param>
    /// <param name="filter">过滤</param>
    /// <typeparam name="T">含有T类型组件</typeparam>
    /// <returns></returns>
    public static List<GameObject> LoadGameObjAtPath<T>(string[] pathArray, string filter)
    {
        var unitObjList = new List<GameObject>();
        var guids = AssetDatabase.FindAssets(filter, pathArray);
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var unitObj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (unitObj.GetComponent<T>() != null)
                unitObjList.Add(unitObj);
        }
        return unitObjList;
    }
}
