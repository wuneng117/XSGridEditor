
/// <summary>
/// @Author: zhoutao
/// @Date: 2022-04-03 14:49:26
/// @Description: 角色属性设置
/// </summary>
using UnityEngine;
using UnityEngine.UI;

namespace XSSLG
{
    public class RolePanelaAttr : MonoBehaviour
    {
        [SerializeField]
        private Text nameText;

        // [SerializeField]
        // private ProgressbarDeterminateBase attrBar;

        [SerializeField]
        private string nameStr;

        private bool InitAttrBar {get; set;} = false;

        public void Start()
        { 
            if (this.nameText)
                this.nameText.text = this.nameStr;
        }

        public void SetValue(int value, int max)
        { 
            this.initAttrBarTextFunc();
            // this.attrBar.Max = max;
            // this.attrBar.Value = value;
        }

        private void initAttrBarTextFunc()
        { 
            if (!this.InitAttrBar)
            {
                // if (this.attrBar)
			    //     this.attrBar.TextFunc = bar => string.Format("{0}", bar.Value.ToString());
                this.InitAttrBar = true;
            }
        }
    }
}
