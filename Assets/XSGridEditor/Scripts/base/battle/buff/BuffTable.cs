using System.Linq;

namespace XSSLG
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/19
/// @Description: buff列表
/// </summary>
{
    /// <summary> buff列表 </summary>
    public class BuffTable : CommonTable<BuffBase, SkillUpdateData>
    {
        public void Add(BuffBase buff)
        {
            do
            {
                var prevBuff = this.List.Find(item => item.Data.Key == buff.Data.Key);
                if (prevBuff != null && buff.Data.CanStack)
                {
                    prevBuff.AddCount(buff.Data.InitCount);
                    buff = prevBuff;
                    break;
                }
                else if (prevBuff != null)
                    prevBuff.Destroy();

                this.List.Add(buff);
                // buff.RegisterEvent(); // buff在加入bufftable后再注册事件，不然事件在上面还要取消注册
                // this._buff.skill.emitSkillEffectTime(EffectTime.OnBuffStart, undefined, enemyTops); // 一定要在这里加啊，不然BUFF叠加，特效也叠加了
                // this._buff.emitBuffEffectTime(EffectTime.OnBuffStart);
            } while (false);

            buff.OnAdd();
        }

        public Stat GetStat()
        {
            var ret = this.List.Aggregate(new Stat(), (ret, buff) => buff.Data.Stat);
            return ret;
        }
    }
}