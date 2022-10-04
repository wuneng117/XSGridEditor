namespace XSSLG
{
    /// <summary> phase常用方法 </summary>
    public class PhaseUtils
    {
        public static void CheckIsMoving<T>(PhaseBase phase, T logic, PhaseBase nextPhase) where T : BattleLogic
        {
            if (!logic.UnitMgr.ActionUnit.Node.IsMoving)
            {
                logic.UnitMgr.ActionUnit.SetMoved();
                XSUG.GetBattleLogic().Change(nextPhase);
            }
        }
    }
}