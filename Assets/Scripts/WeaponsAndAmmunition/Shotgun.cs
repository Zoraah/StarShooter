using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.WeaponsAndAmmunitions
{
    public class Shotgun : Gun
    {
        [SerializeField] private Transform[] _bulletsSpawnPositions = default;

        [SerializeField] private float _randomizedRotationAngles = default;

        public override void Fire(Quaternion rotation)
		{
            if (_readyToFire)
            {
                _gunAnimator.Play($"{SHOOT_ANIMATION}");
                List<Rigidbody> bullets = CreateBullets(rotation);
                SetBulletsDamage(bullets);
                SetBulletsForce(bullets);
                _readyToFire = false;
                OnShootEvent?.Invoke(_energyCost);
            }
        }

		public override void Fire()
		{

		}

		public override void MultipleTimeFire(Transform playerTransform)
		{

		}

		private List<Rigidbody> CreateBullets(Quaternion rotation)
		{
            List<Rigidbody> bullets = new List<Rigidbody>();

            float randomizedX;
            float randomizedY;
            float randomizedZ;

            foreach (Transform spawnPosition in _bulletsSpawnPositions)
			{
                randomizedX = rotation.eulerAngles.x + Random.Range(-_randomizedRotationAngles, _randomizedRotationAngles);
                randomizedY = rotation.eulerAngles.y + Random.Range(-_randomizedRotationAngles, _randomizedRotationAngles);
                randomizedZ = rotation.eulerAngles.z + Random.Range(-_randomizedRotationAngles, _randomizedRotationAngles);

                Quaternion randomizedRotation = Quaternion.Euler(randomizedX, randomizedY, randomizedZ);

                bullets.Add(Instantiate(_bullet, spawnPosition.position, randomizedRotation));
			}

            return bullets;
        }

        private void SetBulletsDamage(List<Rigidbody> bullets)
		{
            foreach(Rigidbody bullet in bullets)
			{
                bullet.GetComponent<Ammunition>().SetBulletDamage(_damage);
			}
		}

        private void SetBulletsForce(List<Rigidbody> bullets)
		{
            foreach (Rigidbody bullet in bullets)
            {
                bullet.AddForce(bullet.transform.forward * _force, ForceMode.Impulse);
            }
        }
    }
}