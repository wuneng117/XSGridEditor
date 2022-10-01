using System;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/30
/// @Description: 各种效果触发条件的管理
/// </summary>
namespace XSSLG
{
    /// <summary> 各种效果触发条件的管理 </summary>
    public class BattleEmitter : Emitter<TriggerDataTriggerType, Action<OnTriggerDataBase>>
    {
        /************************* 变量 begin ***********************/
        private static BattleEmitter msInstance;
        public static BattleEmitter Instance { get => msInstance = msInstance ?? new BattleEmitter(); }
        /************************* 变量  end  ***********************/
    }
}