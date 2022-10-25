using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class XSSkillEditorWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/XSSKillEditorWindow")]
    public static void ShowExample()
    {
        XSSkillEditorWindow wnd = GetWindow<XSSkillEditorWindow>();
        wnd.titleContent = new GUIContent("XSSkillEditorWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/XSSkillEditorWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
}