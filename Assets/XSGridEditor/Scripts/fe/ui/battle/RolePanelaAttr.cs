
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
        protected Text nameText;

        [SerializeField]
        protected ProgressbarLabel attrBar;

        [SerializeField]
        protected string nameStr;

        public void Start()
        { 
            if (this.nameText)
            {
                this.nameText.text = this.nameStr;
            }
        }

        public void SetValue(int value, int max)
        { 
            if (this.attrBar)
            {
                this.attrBar.SetValue(value, max);
            }
        }
    }
}
