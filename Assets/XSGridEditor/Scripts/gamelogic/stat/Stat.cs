using UnityEngine;
using System;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 角色属性
/// </summary>
namespace XSSLG
{
    /// <summary> 攻击啊防御属性集合 </summary>
    [Serializable]
    public class Stat
    {
        /// <summary> 力量 </summary>
        [SerializeField]
        private Attr str = new Attr();
        public Attr Str { get => str; }

        /// <summary> 防守 </summary>
        [SerializeField]
        protected Attr def = new Attr();
        public Attr Def { get => def; }

        /// <summary> 魔力 </summary>
        [SerializeField]
        protected Attr mag = new Attr();
        public Attr Mag { get => mag; }

        /// <summary> 魔防 </summary>
        [SerializeField]
        protected Attr dex = new Attr();
        public Attr Dex { get => dex; }
        
        /// <summary> 移动 </summary>
        [SerializeField]
        protected Attr mov = new Attr();
        public Attr Mov { get => mov; }

        /// <summary> 速度 </summary>
        [SerializeField]
        protected Attr spd = new Attr();
        public Attr Spd { get => spd; }

        /// <summary> 技巧 </summary>
        [SerializeField]
        protected Attr res = new Attr();
        public Attr Res { get => res; }

        /// <summary> 幸运 </summary>
        [SerializeField]
        protected Attr lck = new Attr();
        public Attr Lck { get => lck; }

        /// <summary> 魅力 </summary>
        [SerializeField]
        protected Attr cha = new Attr();
        public Attr Cha { get => cha; }

        [SerializeField]
        protected HP hp = new HP();
        public HP HP { get => hp; }

        // private HP _MP = new HP();

        public Stat()
        {
        }

        public Stat(Stat stat)
        {
            this.Clone(stat);
        }

        /// <summary> 加其它属性 </summary>
        public bool Add(Stat stat)
        {
            if (stat == null)
            {
                return false;
            }
            this.str.Add(stat.str);
            this.def.Add(stat.def);
            this.mag.Add(stat.mag);
            this.dex.Add(stat.dex);
            this.mov.Add(stat.mov);
            this.spd.Add(stat.spd);
            this.res.Add(stat.res);
            this.lck.Add(stat.lck);
            this.cha.Add(stat.cha);
            this.hp.Add(stat.hp);
            return true;
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this.str.Reset();
            this.def.Reset();
            this.mag.Reset();
            this.dex.Reset();
            this.mov.Reset();
            this.spd.Reset();
            this.res.Reset();
            this.lck.Reset();
            this.cha.Reset();
            this.hp.Reset();
        }

        /// <summary> 拷贝 </summary>
        public void Clone(Stat stat)
        {
            this.Reset();
            this.Add(stat);
        }
    }
}