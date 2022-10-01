using System;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/30
/// @Description: 触发器触发条件基类
/// </summary>
namespace XSSLG
{
    /// <summary> 触发器触发条件基类 </summary>
    public abstract class TriggerConditionBase
    {
        /************************* 变量 begin ***********************/
        protected TriggerDataConditionStruct ConditionStruct { get; }
        /************************* 变量  end  ***********************/
        public TriggerConditionBase(TriggerDataConditionStruct conditionStruct)
        {
            this.ConditionStruct = conditionStruct;
        }

        /// <summary> 能否释放，如果没有条件当然可以了~ 否则就做检查</summary>
        public bool CanRelease(OnTriggerDataBase data) => this.ConditionStruct == null ? true : this.Check(data);

        /// <summary>
        /// 检查数值是否合法
        /// </summary>
        /// <param name="val">要检查的数值</param>
        protected bool CheckProp(int val)
        {
            if (this.ConditionStruct == null)
                return true;

            switch (this.ConditionStruct.Compare)
            {
                default:
                case CompareType.None: return true;
                case CompareType.LessThan: return val <= this.ConditionStruct.Prop;
                case CompareType.Equal: return val == this.ConditionStruct.Prop;
                case CompareType.MoreThan: return val > this.ConditionStruct.Prop;
            }
        }

        protected abstract bool Check(OnTriggerDataBase data);
    }

    /// <summary> 无条件通过检查 </summary>
    public class TriggerConditionNull : TriggerConditionBase
    {
        public TriggerConditionNull(TriggerDataConditionStruct conditionStruct) : base(conditionStruct) { }
        protected override bool Check(OnTriggerDataBase data) => true;

    }
}