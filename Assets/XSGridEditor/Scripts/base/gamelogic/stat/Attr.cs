using UnityEngine;
using System;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 
/// 属性基类，有值和百分比是因为方便属性做计算，可以直接相加
/// </summary>
namespace XSSLG
{
    [Serializable]
    public class Attr
    {
        public int Val;
        public float Factor;

        public Attr()
        {
            this.Val = 0;
            this.Factor = 0;
        }

        public Attr(int val) : this()
        {
            this.Val = val;
        }

        public Attr(int val, float factor) : this(val)
        {
            this.Factor = factor;
        }

        /// <summary> 基础乘以百分比计算最终属性 </summary>
        public int GetFinal() => (int)(this.Val * (1 + this.Factor));

        /// <summary> 加其它属性 </summary>
        public void Add(Attr attr)
        {
            this.Val += attr.Val;
            this.Factor += attr.Factor;
        }

        /// <summary> 减其它属性 </summary>
        public void Reduce(Attr attr)
        {
            this.Val -= attr.Val;
            this.Factor -= attr.Factor;
        }

        // 重载 + 运算符
        public static Attr operator +(Attr a, Attr b)
        {
            var attr = new Attr();
            attr.Add(a);
            attr.Add(b);
            return attr;
        }

        // 重载 + 运算符
        public static Attr operator -(Attr a, Attr b)
        {
            var attr = new Attr();
            attr.Add(a);
            attr.Reduce(b);
            return attr;
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this.Val = 0;
            this.Factor = 0;
        }
    }
}