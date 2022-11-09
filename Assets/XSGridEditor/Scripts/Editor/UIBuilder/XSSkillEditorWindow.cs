
using UnityEngine;

namespace XSSLG
{
    public class XSSkillEditorWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = XSEditorDefine.UI_BUILDER_UXML_PATH + "XSSkillEditorWindow.uxml";

        public static void ShowExample()
        {
            var wnd = GetWindowWithRect<XSSkillEditorWindow>(new Rect(0, 0, 800, 350), false, "XSSkillEditor");
            // wnd.Init();
            wnd.ShowModal();
        }
    }
}