using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @Author: zhoutaoz'z
/// @Date: 2021/6/6
/// @Description: 管理界面类
/// </summary>
namespace XSSLG
{
    /// <summary>  </summary>
    public class UIMgr
    {
        /************************* 变量 begin ***********************/
        /// <summary> 
        /// 显示界面时先把栈顶的界面关闭，然后新界面添加到栈顶并显示
        /// 鼠标右键关闭界面时先把栈顶的界面关闭并移除，然后新的栈顶界面显示
        /// </summary>
        private Stack<GameObject> ObjStack { get; set; } = new Stack<GameObject>();

        /************************* 变量  end  ***********************/

        /// <summary> 是否有界面 </summary>
        public bool IsEmpty => this.ObjStack.Count == 0;


        /// <summary> 先把栈顶的界面关闭，然后新界面添加到栈顶并显示 </summary>
        public void ShowUI(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            this.SafePeek()?.SetActive(false);
            obj.SetActive(true);
            this.ObjStack.Push(obj);
        }

        /// <summary> 先把栈顶的界面关闭并移除，然后新的栈顶界面显示 </summary>
        public GameObject CloseUI()
        {
            GameObject ret = this.SafePop();
            ret?.SetActive(false);

            this.SafePeek()?.SetActive(true);
            return ret;
        }

        /// <summary> 从栈顶开始关闭界面，直到关闭obj界面为止 </summary>
        public void CloseUITo(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            while (this.ObjStack.Count != 0)
            {
                var next = this.ObjStack.Pop();
                next.SetActive(false);
                if (next == obj)
                    break;
            }
        }

        private GameObject SafePop() => this.ObjStack.Count != 0 ? this.ObjStack.Pop() : null;
        private GameObject SafePeek() => this.ObjStack.Count != 0 ? this.ObjStack.Peek() : null;
    }
}