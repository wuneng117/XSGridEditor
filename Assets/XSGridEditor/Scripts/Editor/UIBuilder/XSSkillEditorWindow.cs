
using UnityEngine;

namespace XSSLG
{
    public class XSSkillEditorWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/XSSkillEditorWindow.uxml";

        public static void ShowExample()
        {
            var wnd = GetWindowWithRect<XSSkillEditorWindow>(new Rect(0, 0, 800, 350), false, "XSSkillEditor");
            // wnd.Init();
            wnd.ShowModal();
        }
    }
}