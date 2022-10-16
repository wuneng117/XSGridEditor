using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XSSLG
{

    public class ProgressbarLabel : MonoBehaviour
    {

        [SerializeField]
        protected TMP_Text text;

        [SerializeField]
        protected Image bar;

        public virtual void SetValue(int value, int max)
        {
            if (this.text)
            {
                this.text.text = $"{value}/{max}";
            }

            if (this.bar)
            {
                if (max != 0)
                {
                    this.bar.fillAmount = Mathf.Clamp01(value / max);
                }
                else
                {
                    this.bar.fillAmount = 0;
                }
            }
        }
    }
}
