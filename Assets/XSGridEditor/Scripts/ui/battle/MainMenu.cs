/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/11
/// @Description: 主菜单
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XSSLG
{

    public class MainMenu : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /************************* 按钮事件 begin ***********************/
        public void OnClickTurnOver()
        {
            var logic = XSUG.GetBattleLogic();
            this.OnClickClose();
            logic.Change(new PhaseTurnEnd());
        }

        public void OnClickClose()
        {
            BattleNode battleNode = XSUG.GetBattleNode();
            battleNode.CloseMainMenu();
        }

        /************************* 按钮事件  end  ***********************/
    }
}
