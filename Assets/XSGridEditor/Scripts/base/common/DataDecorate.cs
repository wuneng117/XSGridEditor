/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/19
/// @Description: 数据包装
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 数据包装
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class DataDecorate<T>
    {
        protected T Data { get; private set; }

        public virtual bool Init(T data)
        {
            this.Data = data;
            return true;
        }
    }
}