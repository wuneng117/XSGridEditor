/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 
/// 在Attr的基础上增加一个值表示当前剩余的值，可以表示血量，魔法值
/// </summary>
namespace XSSLG
{
    public class HP : IHP
    {
        private int _val = 0;
        private Attr _max;

        public HP(int val = 0, Attr max = null)
        {
            this._val = val;
            this._max = max;
        }

        /// <summary> 基础属性 </summary>
        public int Get() => this._val;

        /// <summary> 基础属性,和Get是一样的，只是为了和IAttr用起来一样 </summary>
        public int GetBase() => this.Get();
        /// <summary> 基础属性,和Get是一样的，只是为了和IAttr用起来一样 </summary>
        public int GetFinal() => this.Get();

        /// <summary> 加其它属性 </summary>
        public void Add(IHP hp)
        {
            this._val += hp.Get();
            this._max.Add(hp.GetMaxAttr());
        }
        
        /// <summary> 重置 </summary>
        public void Reset()
        {
            this._val = 0;
            this._max.Reset();
        }

        /// <summary> 加具体血量 </summary>
        /// TODO 各种验证
        public void Add(int hp)
        {
            this._val += hp;
        }

        /// <summary> 减具体血量 </summary>
        /// TODO 各种验证
        public void Reduce(int hp)
        {
            this._val -= hp;
        }

        /// <summary> 获取最大血量 </summary>
        public int GetMax() => this._max.GetFinal();

        /// <summary> 获取最大血量属性 </summary>
        public IAttr GetMaxAttr() => this._max;

        /// <summary> 获取百分比：血量/最大血量 </summary>
        public float GetPercent() => this._val / this.GetMax();

    }
}