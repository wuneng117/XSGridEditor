namespace XSSLG
{
    /// <summary> 系统级别的常用方法 </summary>
    public class XSSLG
    {
        /// <summary>
        /// 断言
        /// </summary>
        /// <param name="condition">断言条件</param>
        /// <param name="str">断言输出</param>
        public static void Assert(bool condition, string str = "")
        {
            if (!condition) throw new System.ArgumentNullException(str);
        }

        /// <summary>
        /// 8位以内的string转为2进制的long
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static long StringToLong(string str)
        {
            if (str.Length > 8)
                return 0;

            long ret = 0;
            for (int index = 0; index < str.Length; index++)
                ret = (ret << 8) + str[index];
            return ret;
        }

        /// <summary> 二进制的标志位 </summary>
        /// <summary>
        /// 返回二进制的标志位
        /// </summary>
        /// <param name="x">第几位</param>
        public static int Bit(int x) => 1 << x;

        /// <summary>
        /// 清除标记
        /// </summary>
        /// <param name="x">存所有标记的值</param>
        /// <param name="flag">要清除的标记</param>
        public static void ClearFlag(ref int x, int flag)
        {
            x ^= flag;
            x &= (~flag);
        }

        /// <summary>
        /// 添加标记
        /// </summary>
        /// <param name="x">存所有标记的值</param>
        /// <param name="flag">要添加的标记</param>
        public static void SetFlag(ref int x, int flag) => x |= flag;

        /// <summary>
        /// 判断是否有标记
        /// </summary>
        /// <param name="x">存所有标记的值</param>
        /// <param name="flag">要判断的标记</param>
        public static bool GetFlag(ref int x, int flag) => (x & flag) != 0;
    }
}