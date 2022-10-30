using System;
/// <summary>
/// @Author: zhoutao
/// @Date: 2022/10/30
/// @Description: 角色二级属性
/// </summary>
namespace XSSLG
{
    /// <summary> 攻击啊防御属性集合 </summary>
    [Serializable]
    public class SecondStat
    {
        public int HitRate = 0;
        public int DodgeRate = 0;
        public int CritRate = 0;
        public int CritDodgeRate = 0;
        public int DamageRate = 0;
        public int DamageReduceRate = 0;

        public SecondStat()
        {
        }

        public SecondStat(SecondStat stat)
        {
            this.Clone(stat);
        }

        /// <summary> 加其它属性 </summary>
        public bool Add(SecondStat stat)
        {
            if (stat == null)
            {
                return false;
            }
            this.HitRate += stat.HitRate;
            this.DodgeRate += stat.DodgeRate;
            this.CritRate += stat.CritRate;
            this.CritDodgeRate += stat.CritDodgeRate;
            this.DamageRate += stat.DamageRate;
            this.DamageReduceRate += stat.DamageReduceRate;
            return true;
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this.HitRate = 0;
            this.DodgeRate = 0;
            this.CritRate = 0;
            this.CritDodgeRate = 0;
            this.DamageRate = 0;
            this.DamageReduceRate = 0;
        }

        /// <summary> 拷贝 </summary>
        public void Clone(SecondStat stat)
        {
            this.Reset();
            this.Add(stat);
        }
    }
}