/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 
/// 属性基类，有值和百分比是因为方便属性做计算，可以直接相加
/// </summary>
namespace XSSLG
{
    public class Attr : IAttr
    {
        private int _val = 0;
        private float _factor = 0;

        public Attr(int val = 0, float factor = 0)
        {
            this._val = val;
            this._factor = factor;
        }

        /// <summary> 基础属性 </summary>
        public int GetBase() => this._val;

        /// <summary> 加成百分比 </summary>
        public float GetFactor() => this._factor;

        /// <summary> 基础乘以百分比计算最终属性 </summary>
        public int GetFinal() => (int)(this._val * (1 + this._factor));

        /// <summary> 加其它属性 </summary>
        public void Add(IAttr attr)
        {
            this._val += attr.GetBase();
            this._factor += attr.GetFactor();
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this._val = 0;
            this._factor = 0;
        }
    }
}