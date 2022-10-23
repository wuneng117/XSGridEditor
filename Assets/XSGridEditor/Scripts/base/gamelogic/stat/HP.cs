/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 
/// 在Attr的基础上增加一个值表示当前剩余的值，可以表示血量，魔法值
/// </summary>
using System;
using UnityEngine;
namespace XSSLG
{
    [Serializable]
    public class HP
    {
        public int Val;
        public Attr Max;

        public HP()
        {
            this.Val = 0;
            this.Max = new Attr();
        }

        public HP(int val) : this()
        {
            this.Val = val;
        }

        public HP(int val, Attr max) : this(val)
        {
            this.Max = max;
        }

        /// <summary> 基础属性,和Get是一样的，只是为了和IAttr用起来一样 </summary>
        public int GetFinal() => this.Val;

        /// <summary> 加其它属性 </summary>
        public void Add(HP hp)
        {
            this.Val += hp.Val;
            this.Max += hp.Max;
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this.Val = 0;
            this.Max.Reset();
        }

        /// <summary> 加具体血量 </summary>
        /// TODO 各种验证
        public void Add(int hp)
        {
            this.Val += hp;
        }

        /// <summary> 减具体血量 </summary>
        /// TODO 各种验证
        public void Reduce(int hp)
        {
            this.Val -= hp;
        }

        /// <summary> 获取最大血量 </summary>
        public int GetMax() => this.Max.GetFinal();

        /// <summary> 获取百分比：血量/最大血量 </summary>
        public float GetPercent() => this.Val / this.GetMax();

    }
}