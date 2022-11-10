using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.WeaponsAndAmmunitions
{
    public class Machinegun : Gun
    {
        [SerializeField] private const float DEFAULT_FIRE_DELAY = 0.2f;

        private float _fireDelay = DEFAULT_FIRE_DELAY;

        private void Update()
        {
            if(IsFire)
            {
                _fireDelay -= Time.deltaTime;
                if (_fireDelay <= 0)
                {
                    SpawnBullet();
                    _fireDelay = DEFAULT_FIRE_DELAY;
                }
            }
        }

		public override void Fire(Quaternion direction)
		{
            
		}

		public override void Fire()
		{

		}

        private void SpawnBullet()
        {
            Rigidbody bullet = Instantiate(_bullet, _bulletSpawnPosition.position, Direction);
            bullet.AddForce(bullet.transform.forward * _force, ForceMode.Impulse);
        }

        private Quaternion LookAtObject(Transform transform)
        {
            Vector3 lookVector = transform.position - _bulletSpawnPosition.position;
            return Quaternion.LookRotation(lookVector, Vector3.up);
        }
    }
}