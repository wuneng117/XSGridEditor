/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态单位移动中
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 单位移动中
    /// </summary>
    public class PhaseUnitMove : PhaseBase
    {
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            XSUG.CameraCanFreeMove(false);
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            XSUG.CameraCanFreeMove(true);
        }

        public override void Update<T>(T logic)
        {
            base.Update(logic);
            PhaseUtils.CheckIsMoving(this, logic, new PhaseUnitMenu());
        }
    }
}