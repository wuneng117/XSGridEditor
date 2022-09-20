 
using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using XSSLG;

public class XSAssetPostprocessor :AssetPostprocessor
{
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets,string[] movedAssets, string[] movedFromAssetPaths) 
	{
		foreach(string s in importedAssets)
		{
			if (s.Equals("Assets/XSGridEditor/Scripts/base/3d/IGridMgr.cs"))
			{
                CheckLayer();
                return;
			}
		}   
	}

    public static void CheckLayer()
    {
        if (!HasLayer(XSGridDefine.LAYER_TILE))
        {
            AddLayer(XSGridDefine.LAYER_TILE);
        }

        if (!HasLayer(XSGridDefine.LAYER_UNIT))
        {
            AddLayer(XSGridDefine.LAYER_UNIT);
        }
    }

    static bool HasLayer(string layer) => LayerMask.NameToLayer(layer) != -1;

    private static void AddLayer(string layer)
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty it = tagManager.GetIterator();
        while (it.NextVisible(true))
        {
            if (it.name != "layers")
            {
                continue;
            }

            for (int i = 0; i < it.arraySize; i++)
            {
                SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                if (string.IsNullOrEmpty(dataPoint.stringValue)) 
                {
                    dataPoint.stringValue = layer;
                    tagManager.ApplyModifiedProperties();
                    return;
                }
            }
        }
    }
}
