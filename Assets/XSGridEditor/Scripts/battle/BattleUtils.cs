using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/9
/// @Description: 常用的战斗方法
/// </summary>
namespace XSSLG
{
    /// <summary> 常用的战斗方法 </summary>
    public class BattleUtils
    {
        /// <summary>
        /// 根据坐标获取方向,原点为左下角
        /// </summary>
        /// <param name="src">原坐标,世界坐标</param>
        /// <param name="dst">目标坐标,世界坐标</param>
        /// <returns>方向</returns>
        public static DirectionType GetDirection(Vector2 src, Vector2 dst)
        {
            var offset = dst - src;
            if (offset.y > 0)
            {
                if (offset.x >= 0)
                    return DirectionType.RightTop;
                else
                    return DirectionType.LeftTop;
            }
            else
            {
                if (offset.x >= 0)
                    return DirectionType.RightBottom;
                else
                    return DirectionType.LeftBottom;
            }
        }

        /// <summary> 通过tile的地图生成unit，后期需要放到单独的文件比较好，目前代码不多先放这里TODO </summary>
        /// TODO 生成Unitdui
        public static List<Unit> CreateUnitList()
        {
            var ret = new List<Unit>();
            // var gridHelper = XSUG.GetBattleLogic().GridHelper;

            // var objArray = gridHelper.UnitRoot.GetComponentsInChildren<XSUnitData>();

            // objArray.ToList().ForEach(tile =>
            // {
            //     var objData = tile.GetComponent<XSUnitData>();
            //     // 删除子节点，一般是编辑器下显示用的
            //     XSU.RemoveChildren(objData.gameObject);
            //     var unit = objData.GetUnit();
            //     if (unit != null)
            //     {
            //         unit.Node.transform.parent = objData.transform;
            //         ret.Add(unit);
            //     }
            //     // 在游戏里就直接删除吧，错误的object数据
            //     else if (!XSU.IsEditor())  
            //         GameObject.Destroy(objData.gameObject);
            // });

            return ret;
        }
    }
}