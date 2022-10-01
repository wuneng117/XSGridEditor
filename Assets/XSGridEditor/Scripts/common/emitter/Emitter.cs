using System.Linq;
using System;
using System.Collections.Generic;

namespace XSSLG
{
    /// <summary>
    /// 事件分发，给对象之间解耦用的
    /// 在一个Emitter里可以为多个事件注册事件回调，每一个事件还可以对应多个回调函数
    /// 建议1个Emitter对应同一类的事件类型，防止过于臃肿导致查询耗时
    /// 具体用例：
    // class HealReleaseData 
    // {
    // }
    //  class Trigger1 
    //  {
    //      public Trigger(Emitter<TriggerBaseType, Action<HealReleaseData>> emitter)
    //      {
    //          emitter.on(TriggerBaseType.OnApplyDamage, this.onHeal1);
    //      }
    //      void onHeal1(HealReleaseData heal) 
    //      {
    //          Debug.Log("OnApplyDamage");
    //      }
    //  }
    //  class Trigger2
    //  {
    //      public Trigger(Emitter<TriggerBaseType, Action<HealReleaseData>> emitter)
    //      {
    //          emitter.on(TriggerBaseType.OnCauseDamage, this.onHeal2);
    //      }
    //      void onHeal2(HealReleaseData heal) 
    //      {
    //          Debug.Log("OnCauseDamage");
    //      }
    //  }
    //  public main() 
    //  {
    //      Emitter<TriggerBaseType, Action<HealReleaseData>> emitter;
    //      var trigger1 = new Trigger(emitter);
    //      var trigger2 = new Trigger(emitter);
    //      var data = new HealReleaseData();
    //      emitter.emit(TriggerBaseType.OnApplyDamage, data);
    //      emitter.emit(TriggerBaseType.OnCauseDamage, data);
    //  }
    /// </summary>
    /// <typeparam name="TYPE">事件类型，通常是字符串</typeparam>
    /// <typeparam name="T">回调函数类型</typeparam>
    public class Emitter<TYPE, T> where T : Delegate
    {
        private Dictionary<TYPE, EmitterItems<T>> _emitList;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Emitter()
        {
            this._emitList = new Dictionary<TYPE, EmitterItems<T>>();
        }

        /// <summary>
        /// 为事件注册回调
        /// </summary>
        /// <typeparam name="type">事件类型，通常是字符串</typeparam>
        /// <param name="callback">事件回调</param>
        /// <param name="priority">优先级，同一事件中优先级从高到低依次调用</param>
        /// <param name="target"></param>
        public void On(TYPE type, T callback, int priority = 65536, object target = null)
        {
            if (this._emitList.ContainsKey(type) == false)
            {
                this._emitList[type] = new EmitterItems<T>();
            }
            this._emitList[type].SetSlot(callback, priority, target);
        }

        /// <summary>
        /// 触发事件回调
        /// </summary>
        /// <typeparam name="type">事件类型，通常是字符串</typeparam>
        /// <param name="parameters">事件回调带的参数</param>
        public void Emit(TYPE type, params object[] parameters)
        {
            if (this._emitList.ContainsKey(type))
                this._emitList[type].Emit(parameters);
        }
        
        /// <summary>
        /// 为事件取消回调
        /// </summary>
        /// <typeparam name="type">事件类型，通常是字符串</typeparam>
        public void Off(TYPE type)
        {
            if (this._emitList.ContainsKey(type) == false)
                return;

            this._emitList[type].Clear();
        }

        /// <summary>
        /// 为事件取消回调
        /// </summary>
        /// <typeparam name="type">事件类型，通常是字符串</typeparam>
        /// <param name="callback">事件回调</param>
        public void Off(TYPE type, T callback)
        {
            if (this._emitList.ContainsKey(type) == false)
                return;

            this._emitList[type].RemoveSlot(callback);
        }

        /// <summary>
        /// 为事件取消回调，如果是匿名函数，就只能用target去查了
        /// </summary>
        /// <typeparam name="type">事件类型，通常是字符串</typeparam>
        /// <param name="target">在哪个对象上注册</param>
        public void Off(TYPE type, object target)
        {
            if (this._emitList.ContainsKey(type) == false)
                return;

            this._emitList[type].RemoveSlot(target);
        }

        /// <summary>
        /// 为事件取消回调
        /// </summary>
        /// <param name="target">在哪个对象上注册</param>
        public void Off(object target) => this._emitList.Values.ToList().ForEach(emit => emit.RemoveSlot(target));
    }
}
