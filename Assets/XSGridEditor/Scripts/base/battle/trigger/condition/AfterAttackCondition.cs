/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/30
/// @Description: 在攻击后条件判断
/// </summary>
namespace XSSLG
{
    /// <summary> 在攻击后条件判断 </summary>
    public class AfterAttackCondition : TriggerConditionBase
    {
        public AfterAttackCondition(ConditionStruct conditionStruct) : base(conditionStruct){}
        protected override bool Check(OnTriggerDataBase data)
        {
            // 必须传入的是攻击触发数据
            var attackReleaseData = (data as OnTriggerDataAttack);
            if (attackReleaseData == null)
                return false;

            switch (this.ConditionStruct.Type)
            {
                case XSDefine.TriggerConditionType.SelfCauseDamage:return attackReleaseData.SrcCauseDamage.Exists(damage => this.CheckProp(damage));
                default: return true;
            }
        }
    }
}