using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class SkillDamageWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/SkillDamageWindow")]
    public static void ShowExample()
    {
        SkillDamageWindow wnd = GetWindow<SkillDamageWindow>();
        wnd.titleContent = new GUIContent("SkillDamageWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/SkillDamageWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
}