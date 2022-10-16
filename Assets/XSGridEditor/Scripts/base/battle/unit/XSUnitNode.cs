/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/9
/// @Description: script added to unit gameobject
/// </summary>
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> unit data </summary>
    public class XSUnitNode : MonoBehaviour, XSIUnitNode
    {
        // /// <summary> 飘字位置 </summary>
        // private Transform TextDamageTransform;

        // /// <summary> 动画控制 </summary>
        // private UnitAnimation Animation { get; set; }
        
        [SerializeField]
        protected RoleData data = new RoleData();
        public RoleData Data { get => data; set => data = value; }

        /// <summary> 单位势力 </summary>
        public GroupType Group = GroupType.Self;

        public Dictionary<Vector3, List<Vector3>> CachedPaths { get; protected set; }

        public Vector3 WorldPos { get => this.transform.position; set => this.transform.position = value; }

        protected int movementAnimationSpeed = 2;

        /// <summary> 是否正在移动 </summary>
        public bool IsMoving { get; private set; }

        /// <summary> 是否正在播放攻击动画 </summary>
        public bool IsAttacking { get; private set; }

        /// <summary> 是否需要受击后播放死亡动画 </summary>
        public bool IsNeedDeadAnimation { get; set; }

        private void Awake()
        {
            // var animator = this.GetComponent<Animator>();
            // Debug.Assert(animator != null, nameof(animator));
            // this.Animation = new UnitAnimation(animator);
        }

        public virtual Unit GetUnit()
        {
            var unit = UnitFactory.CreateUnit(this.data, this.Group, this);
            if (unit == null)
                return null;

            return unit;
        }

        public virtual void AddBoxCollider()
        {
            var collider = this.gameObject.AddComponent<BoxCollider>();
            var bounds = this.GetMaxBounds();
            collider.bounds.SetMinMax(bounds.min, bounds.max);
            collider.center = collider.transform.InverseTransformPoint(bounds.center);
            collider.size = bounds.size;
        }

        protected virtual Bounds GetMaxBounds()
        {
            var renderers = this.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                return new Bounds();
            }

            var ret = renderers[0].bounds;
            foreach (Renderer r in renderers)
            {
                ret.Encapsulate(r.bounds);
            }

            return ret;
        }



        public virtual void RemoveNode() => XSU.RemoveObj(this.gameObject);

        public virtual bool IsNull() => this == null;

        public virtual void UpdatePos()
        {
            XSU.GridHelper.SetTransToTopTerrain(this.transform, true);
        }

        public void RotateTo(XSTile tile) => this.transform.localRotation = XSU.RotationTo(this.transform.position, tile.WorldPos);

        /// <summary>
        /// move to the destination
        /// </summary>
        /// <param name="path">move path</param>
        public void WalkTo(List<Vector3> path)
        {
            if (this.movementAnimationSpeed > 0)
            {
                StartCoroutine(MovementAnimation(path));
            }
        }

        /// <summary> Coroutine to deal move </summary>
        public virtual IEnumerator MovementAnimation(List<Vector3> path)
        {
            this.IsMoving = true;
            path.Reverse(); // reverse the path
            foreach (var pos in path)
            {
                while (this.transform.position != pos)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, pos, Time.deltaTime * movementAnimationSpeed);
                    yield return 0;
                }
            }
            this.IsMoving = false;
        }

         /// <summary> 攻击动画 </summary>
        public void AttackAnimation()
        {
            //// this.IsAttacking = true;
            // this.Animation.PlayAttack();
            //// this.vcam.gameObject.SetActive(true);
        }

        /// <summary> 受击动画 </summary>
        public void ApplyDamageAnimation(int damage)
        {
            // this.Animation.PlayApplyDamage();
            // XSU.ShowDamageText(damage.ToString(), this.TextDamageTransform);
        }

        /// <summary> 死亡动画 </summary>
        public void DieAnimation() {}
        // public void DieAnimation() => this.Animation.PlayDie();
    }
}
