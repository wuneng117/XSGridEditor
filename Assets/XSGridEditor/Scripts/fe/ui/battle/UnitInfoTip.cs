using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XSSLG
{
    public class UnitInfoTip : MonoBehaviour
    {
        [SerializeField]
        /// <summary> 名字 </summary>
        private Text nameText;

        [SerializeField]
        /// <summary> 等级 </summary>
        private Text levelText;

        [SerializeField]
        /// <summary> hp </summary>
        private Text hpText;

        private UnitBase CacheUnit { get; set; }

        public void Open(UnitBase unit)
        {
            this.gameObject.SetActive(true);
            this.CacheUnit = unit;
            this.Refresh(unit);
        }

        public void Refresh() => this.Refresh(this.CacheUnit);
        private void Refresh(UnitBase unit)
        {
            if (unit == null)
                return;
            
            this.nameText.text = unit.Role.Data.Name;
            this.levelText.text = unit.Role.Level.Lv.ToString();
            var hp = unit.GetStat().HP;
            this.hpText.text = hp.GetFinal() + "/" + hp.GetMax();
        }

        public void Close() => this.gameObject.SetActive(false);


    }
}

