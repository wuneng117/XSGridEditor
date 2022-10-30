using UnityEngine;
namespace XSSLG
{
    public class SkillDamageWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/SkillDamageWindow.uxml";

        public static void ShowExample()
        {
            var wnd = GetWindowWithRect<SkillDamageWindow>(new Rect(0, 0, 503, 600), false, "SkillDamageWindow");
            // wnd.Init();
            wnd.ShowModal();
        }
    }
}