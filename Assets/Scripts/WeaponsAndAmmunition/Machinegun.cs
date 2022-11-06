using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.WeaponsAndAmmunitions
{
    public class Machinegun : Gun
    {
        [SerializeField] private const float DEFAULT_FIRE_DELAY = 0.3f;
        [SerializeField] private const int DEFAULT_MULTIPLE_TIME_FIRE_VALUE = 3;

        private float _fireDelay = DEFAULT_FIRE_DELAY;
        private int _multipleTimeFireValue = DEFAULT_MULTIPLE_TIME_FIRE_VALUE;

        private Coroutine _multipleFireCoroutine = default;

		public override void Fire(Quaternion rotation)
		{

		}

		public override void Fire()
		{

		}

		public override void MultipleTimeFire(Transform transform)
        {
            KillCoroutine(_multipleFireCoroutine);
            _multipleFireCoroutine = StartCoroutine(MultipleFire(transform));
        }

        private IEnumerator MultipleFire(Transform playerTransform)
        {
            while (_multipleTimeFireValue > 0)
            {
                _fireDelay -= Time.deltaTime;
                if (_fireDelay <= 0)
                {
                    Quaternion direction = LookAtObject(playerTransform);
                    Fire(direction);
                    _multipleTimeFireValue--;
                    _fireDelay = DEFAULT_FIRE_DELAY;
                }

                yield return null;
            }

            _multipleTimeFireValue = DEFAULT_MULTIPLE_TIME_FIRE_VALUE;
        }

        private Quaternion LookAtObject(Transform transform)
        {
            Vector3 lookVector = transform.position - _bulletSpawnPosition.position;
            return Quaternion.LookRotation(lookVector, Vector3.up);
        }
    }
}