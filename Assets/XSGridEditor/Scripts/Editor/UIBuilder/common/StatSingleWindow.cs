using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class StatSingleWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/StatSingleWindow")]
    public static void ShowExample()
    {
        StatSingleWindow wnd = GetWindow<StatSingleWindow>();
        wnd.titleContent = new GUIContent("StatSingleWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/StatSingleWindow.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);
    }
}