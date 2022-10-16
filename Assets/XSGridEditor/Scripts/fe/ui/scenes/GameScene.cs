using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace XSSLG
{

    public class GameScene : MonoBehaviour, IBottomTip
    {
        /// <summary> 玩家控制角色 </summary>
        public Transform _player;
        public Transform GetPlayer() => this._player;

        /// <summary> 靠近底部的提示文字 </summary>
        public Text _bottomTipText;
        /// <summary> 设置底部提示 </summary>
        public void SetBottomTips(string str) => this._bottomTipText.text = str;

        private void Awake()
        {
            this._bottomTipText.text = "";
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}
