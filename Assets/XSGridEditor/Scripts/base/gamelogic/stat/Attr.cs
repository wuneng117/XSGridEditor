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
        [SerializeField]
        protected int val;
        public int Val { get => val; set => val = value; }

        [SerializeField]
        protected float factor;
        public float Factor { get => factor; set => factor = value; }

        public Attr()
        {
            this.val = 0;
            this.factor = 0;
        }

        public Attr(int val) : this()
        {
            this.val = val;
        }

        public Attr(int val, float factor) : this(val)
        {
            this.factor = factor;
        }

        /// <summary> 基础乘以百分比计算最终属性 </summary>
        public int GetFinal() => (int)(this.val * (1 + this.factor));

        /// <summary> 加其它属性 </summary>
        public void Add(Attr attr)
        {
            this.val += attr.val;
            this.factor += attr.factor;
        }

        /// <summary> 减其它属性 </summary>
        public void Reduce(Attr attr)
        {
            this.val -= attr.val;
            this.factor -= attr.factor;
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
            this.val = 0;
            this.factor = 0;
        }
    }
}