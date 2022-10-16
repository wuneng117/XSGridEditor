
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/9
/// @Description: 鼠标事件监听，并分发给PhaseBase去处理
/// </summary>
namespace XSSLG
{
    /// <summary> 鼠标事件监听，并分发给PhaseBase去处理 </summary>
    public class PhaseMouseEvent
    {
        /************************* 变量 begin ***********************/
        /************************* 变量  end  ***********************/

        /// <summary> 监听鼠标事件，并且分发给phase </summary>
        public void Update<T>(T logic, IPhaseBase phase) where T : BattleLogic
        {
            var mouse = Mouse.current;
            if (Pointer.current.delta.ReadValue() != Vector2.zero || mouse.leftButton.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame)
            {
                var mouseTile = XSU.GetMouseTargetTile();
                if (Pointer.current.delta.ReadValue() != Vector2.zero)
                    phase.OnMouseMove(logic, mouseTile);

                if (mouse.leftButton.wasPressedThisFrame)
                    phase.OnMouseUpLeft(logic, mouseTile);

                if (mouse.rightButton.wasPressedThisFrame)
                    phase.OnMouseUpRight(logic, mouseTile);
            }
        }
    }
}