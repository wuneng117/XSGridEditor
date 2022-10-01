/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 所有等级类的基类，所有通过经验提升等级的都可以继承这个
/// </summary>
using System;
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary> 等级基类 </summary>
    public class LevelBase
    {
        /// <summary> 当前等级 </summary>
        public int Lv { get; private set; }
        /// <summary> 当前经验 </summary>
        public int Exp { get; private set; }
        /// <summary> 经验值数组 </summary>
        public int[] ExpArray { get; }

        public LevelBase(int[] expArray, int lv = 0, int exp = 0)
        {
            this.ExpArray = expArray;
            this.Lv = lv;
            this.Exp = exp;
        }

        /// <summary> 增加经验值并看看能否升级 </summary>
        public bool AddExpAndCheckLevel(int addExp)
        {
            this.Exp += addExp;
            return this.TryLevelUp();
        }

        /// <summary> 尝试升级 </summary>
        private bool TryLevelUp()
        {
            if (this.ExpArray.Length < this.Lv || this.Lv < 0)
                return false;

            if (this.ExpArray[this.Lv] < this.Exp)
                return false;

            this.Exp -= this.ExpArray[this.Lv];
            this.Lv++;
            return true;
        }
    }
}