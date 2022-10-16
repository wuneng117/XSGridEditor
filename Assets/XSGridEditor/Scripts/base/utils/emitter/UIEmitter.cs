using System;

namespace XSSLG
{
    /// <summary> ui刷新事件的参数类型 </summary>
    public class UIEmitterData
    {
        /// <summary> 发出事件的玩家index </summary>
        public int PlayerIndex { get; set; }
    }

    /// <summary> ui刷新事件 </summary>
    public partial class UIEmitter : Emitter<string, Action<UIEmitterData>>
    {
        private static UIEmitter msInstance;
		public static UIEmitter Instance 
        { 
            get => msInstance = msInstance ?? UIEmitterFactory.CreateUIEmitter(); 
        }

        /************************* 阶段事件 begin ***********************/
        public static string UI_PLAYER_GAME_START = "UI_PLAYER_GAMESTART";
        public static string UI_PLAYER_GAME_END = "UI_PLAYER_GAMEEND";
        public static string UI_PLAYER_TURN_BEGIN = "UI_PLAYER_TURNBEGIN";
        public static string UI_PLAYER_TURN_END = "UI_PLAYER_TURNEND";
        /************************* 阶段事件  end  ***********************/

        /************************* 牌变动 begin ***********************/
        /// <summary> 牌组变动 </summary>
        public static string UI_DECK_CHANGED = "UI_DECK_CHANGED";
        /// <summary> 公共牌组变动 </summary>
        public static string UI_PUBLICDECK_CHANGED = "UI_PUBLICDECK_CHANGED";
        /// <summary> 手牌变动 </summary>
        public static string UI_HANDCARDS_CHANGED = "UI_HANDCARDS_CHANGED";
        /// <summary> 出牌区变动 </summary>
        public static string UI_PLAYAREACARDS_CHANGED = "UI_PLAYAREACARDS_CHANGED";
        /// <summary> 弃牌区变动 </summary>
        public static string UI_DISCARDS_CHANGED = "UI_DISCARDS_CHANGED";
        /************************* 牌变动  end  ***********************/
    }
}
