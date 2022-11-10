using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StarShooter.WeaponsAndAmmunitions
{
    public class Gun : MonoBehaviour
    {
        public Transform BulletSpawnerPosition { get { return _bulletSpawnPosition; } }

        public UnityEvent<float> OnShootEvent;
        public UnityEvent OnHideEvent;

        public bool IsFire = default;
        public Quaternion Direction = default;

        [SerializeField] protected Animator _gunAnimator = default;
        [SerializeField] protected const string SHOOT_ANIMATION = "Animation_Shoot";
        
        [SerializeField] protected Transform _bulletSpawnPosition = default;
        [SerializeField] protected Rigidbody _bullet = default;

        [SerializeField] protected int _force = default;
        [SerializeField] protected int _damage = default;
        [SerializeField] protected float _energyCost = default;
        [SerializeField] protected bool _readyToFire = default;
        [SerializeField] protected bool _enableShootAnimations = default; 

        private void OnEnable()
        {
            _readyToFire = false;
        }

        #region Fire

        public virtual void Fire(Quaternion rotation)
		{

		}
        
        public virtual void Fire()
		{
            
		}

        public virtual void MultipleTimeFire(Transform playerTransform)
		{

		}

		#endregion

        public void PlayAnimation(string animationName)
		{
            _gunAnimator.Play(animationName);
		}

        public void DoOnHideOperations()
		{
            OnHideEvent?.Invoke();
            gameObject.SetActive(false);
		}

		public void IsReadyToFire(int isReady)
		{
            _readyToFire = Convert.ToBoolean(isReady);
		}

        protected void PlayShootAnimation()
		{
            if(_enableShootAnimations)
			{
                PlayAnimation(SHOOT_ANIMATION);
                _readyToFire = false;
			}
		}

        protected void SetBulletDamage(Ammunition ammunition)
		{
            ammunition.SetBulletDamage(_damage);
		}

        protected void KillCoroutine(Coroutine coroutine)
		{
            if (coroutine != null)
			{
                StopCoroutine(coroutine);
                coroutine = null;
			}
		}
    }
}