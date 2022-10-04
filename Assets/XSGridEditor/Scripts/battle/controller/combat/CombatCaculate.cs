/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/20
/// @Description: 战斗伤害计算哦哦哦
/// </summary>
namespace XSSLG
{
    /// <summary> 战斗计算 </summary>
    public class CombatCaculate
    {
        public static int ApplyDamage(UnitBase src, UnitBase dest, int damage)
        {
            // 如果有护甲先减少护甲

            var hp = dest.GetStat().GetHP();
            hp.Reduce(damage);
            // todo 计算死亡
            if (hp.GetFinal() <= 0)
                Die((Unit)dest);

            return damage;
        }

        private static void Die(Unit unit)
        {
            var logic = XSUG.GetBattleLogic();

            unit.Die();
        }
    }
}