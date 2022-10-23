using UnityEngine;
using System;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 角色一级属性
/// </summary>
namespace XSSLG
{
    /// <summary> 攻击啊防御属性集合 </summary>
    [Serializable]
    public class Stat
    {
        /// <summary> 力量 </summary>
        public Attr Str = new Attr();

        /// <summary> 防守 </summary>
        public Attr Def = new Attr();

        /// <summary> 魔力 </summary>
        public Attr Mag = new Attr();

        /// <summary> 魔防 </summary>
        public Attr Dex = new Attr();
        
        /// <summary> 移动 </summary>
        public Attr Mov = new Attr();

        /// <summary> 速度 </summary>
        public Attr Spd = new Attr();

        /// <summary> 技巧 </summary>
        public Attr Res = new Attr();

        /// <summary> 幸运 </summary>
        public Attr Lck = new Attr();

        /// <summary> 魅力 </summary>
        public Attr Cha = new Attr();
        
        public HP HP = new HP();

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
            this.Str.Add(stat.Str);
            this.Def.Add(stat.Def);
            this.Mag.Add(stat.Mag);
            this.Dex.Add(stat.Dex);
            this.Mov.Add(stat.Mov);
            this.Spd.Add(stat.Spd);
            this.Res.Add(stat.Res);
            this.Lck.Add(stat.Lck);
            this.Cha.Add(stat.Cha);
            this.HP.Add(stat.HP);
            return true;
        }

        /// <summary> 重置 </summary>
        public void Reset()
        {
            this.Str.Reset();
            this.Def.Reset();
            this.Mag.Reset();
            this.Dex.Reset();
            this.Mov.Reset();
            this.Spd.Reset();
            this.Res.Reset();
            this.Lck.Reset();
            this.Cha.Reset();
            this.HP.Reset();
        }

        /// <summary> 拷贝 </summary>
        public void Clone(Stat stat)
        {
            this.Reset();
            this.Add(stat);
        }
    }
}