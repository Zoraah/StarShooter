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

        [SerializeField] protected Animator _gunAnimator = default;
        [SerializeField] protected const string SHOOT_ANIMATION = "Animation_Shoot";
        
        [SerializeField] protected Transform _bulletSpawnPosition = default;
        [SerializeField] protected Rigidbody _bullet = default;

        [SerializeField] protected int _force = default;
        [SerializeField] protected int _damage = default;
        [SerializeField] protected float _energyCost = 10;
        [SerializeField] protected bool _readyToFire = default;

        private void OnEnable()
        {
            _readyToFire = false;
        }

        #region Fire

        public virtual void Fire(Quaternion rotation)
		{
            Rigidbody bullet = Instantiate(_bullet, _bulletSpawnPosition.position, rotation);
            SetBulletDamage(_bullet.GetComponent<Ammunition>());
            bullet.AddForce(bullet.transform.forward * _force, ForceMode.Impulse);

            OnShootEvent?.Invoke(_energyCost);
        }

        public virtual void Fire()
		{
            Rigidbody bullet = Instantiate(_bullet, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
            SetBulletDamage(_bullet.GetComponent<Ammunition>());
            bullet.AddForce(bullet.transform.forward * _force, ForceMode.Impulse);

            Debug.Log("Gun fire");
		}

        public virtual void MultipleTimeFire(Transform playerTransform)
        {

        }

		#endregion

		#region Animations

        public void PlayAnimation(string animationName)
		{
            _gunAnimator.Play(animationName);
		}

        public void DoOnHideOperations()
		{
            OnHideEvent?.Invoke();
            gameObject.SetActive(false);
		}

		#endregion

		public void IsReadyToFire(int isReady)
		{
            _readyToFire = Convert.ToBoolean(isReady);
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