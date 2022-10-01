
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 角色属性
/// </summary>
namespace XSSLG
{
    /// <summary> 攻击啊防御属性集合 </summary>
    public class Stat
    {
        /// <summary> 力量 </summary>
        private Attr _str;
        /// <summary> 防守 </summary>
        private Attr _def;
        /// <summary> 魔力 </summary>
        private Attr _mag;
        /// <summary> 魔防 </summary>
        private Attr _dex;
        /// <summary> 移动 </summary>
        private Attr _mov;
        /// <summary> 速度 </summary>
        private Attr _spd;
        /// <summary> 技巧 </summary>
        private Attr _res;
        /// <summary> 幸运 </summary>
        private Attr _lck;
        /// <summary> 魅力 </summary>
        private Attr _cha;
        private HP _HP = new HP();
        // private HP _MP = new HP();

        // 用表格里属性类型当参数传进去
        public Stat(StatData data = null)
        {
            this.Init(data);
        }


        public Stat(Stat stat)
        {
            this.Init();
            this.Clone(stat);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data"></param>
        private void Init(StatData data = null)
        {
            data = data ?? new StatData();
            this._str = new Attr(data.Str, data.StrFactor);
            this._def = new Attr(data.Def, data.DefFactor);
            this._mag = new Attr(data.Mag, data.MagFactor);
            this._dex = new Attr(data.Dex, data.DexFactor);
            this._mov = new Attr(data.Mov, data.MovFactor);
            this._spd = new Attr(data.Spd, data.SpdFactor);
            this._res = new Attr(data.Res, data.ResFactor);
            this._lck = new Attr(data.Lck, data.LckFactor);
            this._cha = new Attr(data.Cha, data.ChaFactor);
            this._HP = new HP(data.Hp, new Attr(data.Hp, data.HpFactor));
        }

        public Attr GetStr() => this._str;
        public Attr GetDef() => this._def;
        public Attr GetMag() => this._mag;
        public Attr GetDex() => this._dex;
        public Attr GetMov() => this._mov;
        public Attr GetSpd() => this._spd;
        public Attr GetRes() => this._res;
        public Attr GetLck() => this._lck;
        public Attr GetCha() => this._cha;
        public HP GetHP() => this._HP;
        // public HP GetMP() => this._MP;


        /// <summary> 加其它属性 </summary>
        public bool Add(Stat stat)
        {
            this._str.Add(stat._str);
            this._def.Add(stat._def);
            this._mag.Add(stat._mag);
            this._dex.Add(stat._dex);
            this._mov.Add(stat._mov);
            this._spd.Add(stat._spd);
            this._res.Add(stat._res);
            this._lck.Add(stat._lck);
            this._cha.Add(stat._cha);
            this._HP.Add(stat._HP);
            return true;
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this._str.Reset();
            this._def.Reset();
            this._mag.Reset();
            this._dex.Reset();
            this._mov.Reset();
            this._spd.Reset();
            this._res.Reset();
            this._lck.Reset();
            this._cha.Reset();
            this._HP.Reset();
        }

        /// <summary> 拷贝 </summary>
        public void Clone(Stat stat)
        {
            this.Reset();
            this.Add(stat);
        }
    }
}