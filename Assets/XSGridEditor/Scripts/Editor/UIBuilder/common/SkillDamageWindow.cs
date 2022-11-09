using UnityEngine;
namespace XSSLG
{
    public class SkillDamageWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = XSEditorDefine.UI_BUILDER_UXML_PATH + "common/SkillDamageWindow.uxml";

        public static void ShowExample()
        {
            var wnd = GetWindowWithRect<SkillDamageWindow>(new Rect(0, 0, 503, 600), false, "SkillDamageWindow");
            // wnd.Init();
            wnd.ShowModal();
        }
    }
}