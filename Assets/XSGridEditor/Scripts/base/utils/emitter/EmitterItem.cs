using System;
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary>
    /// 1个回调函数的类型结构
    /// </summary>
    /// <typeparam name="T">回调函数类型</typeparam>
    class EmitterItem<T> where T : Delegate
    {
        public T callback;
        public int priority;
        public object target;
    }

    /// <summary>
    /// 1个事件对应的多个回调函数结构
    /// </summary>
    /// <typeparam name="T">回调函数类型</typeparam>
    class EmitterItems<T> where T : Delegate
    {
        List<EmitterItem<T>> _slotArray;

        /// <summary>
        /// 构造函数
        /// </summary>
        public EmitterItems()
        {
            this._slotArray = new List<EmitterItem<T>>();
        }

        /// <summary>
        /// 查找回调函数结构
        /// </summary>
        /// <param name="callback">函数对象</param>
        /// <returns></returns>
        private EmitterItem<T> FindSlot(T callback)
        {
            if (callback == null)
                return null;

            EmitterItem<T> ret = this._slotArray.Find(item => item.callback == callback);
            return ret;
        }

        /// <summary>
        /// 添加回调函数结构
        /// </summary>
        /// <param name="callback">函数对象</param>
        /// <param name="priority">优先级</param>
        /// <param name="target">函数注册在哪个对象（可能不需要这个参数）</param>
        /// <returns></returns>
        internal void SetSlot(T callback, int priority, object target = null)
        {
            if (callback == null)
                return;

            // 已经注册过了就不要注册了
            EmitterItem<T> ret = this.FindSlot(callback);
            if (ret != null)
                return;

            EmitterItem<T> newItem = new EmitterItem<T>();
            newItem.callback = callback;
            newItem.priority = priority;
            newItem.target = target;

            this._slotArray.Add(newItem);
            this._slotArray.Sort((a, b) => a.priority - b.priority);
        }

        /// <summary>
        /// 触发事件回调
        /// </summary>
        /// <param name="parameters">回调的参数</param>
        internal void Emit(params object[] parameters) => this._slotArray.ForEach(item => item.callback.DynamicInvoke(parameters));

        /// <summary>
        /// 移除全部事件回调
        /// </summary>
        internal void Clear() => this._slotArray.Clear();

        /// <summary>
        /// 移除事件回调
        /// </summary>
        /// <param name="callback"></param>
        internal void RemoveSlot(T callback)
        {
            if (callback == null)
                return;

            this._slotArray.RemoveAll(item => item.callback == callback);
        }

        /// <summary>
        /// 移除事件回调
        /// </summary>
        /// <param name="target"></param>
        internal void RemoveSlot(object target)
        {
            if (target == null)
                return;

            this._slotArray.RemoveAll(item => item.target == target);
        }
    }
}