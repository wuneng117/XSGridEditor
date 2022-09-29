using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using XSSLG;

public class XSUE : XSUnityUtils
{
    protected XSUE() {}


    /// <summary>
    /// Generate a textmeshpro font node that fits the size, the default font used temporarily
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
    /// load the contents of the folder
    /// </summary>
    /// <param name="pathArray">load folder path</param>
    /// <param name="filter"></param>
    /// <typeparam name="T">Contains T type script</typeparam>
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
            {
                unitObjList.Add(unitObj);
            }
        }
        return unitObjList;
    }
}
