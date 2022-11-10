using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.WeaponsAndAmmunitions
{
    public class Pistol : Gun
    {
        public override void Fire(Quaternion rotation)
        {
            Rigidbody bullet = Instantiate(_bullet, _bulletSpawnPosition.position, rotation);
            SetBulletDamage(_bullet.GetComponent<Ammunition>());
            bullet.AddForce(bullet.transform.forward * _force, ForceMode.Impulse);

            OnShootEvent?.Invoke(_energyCost);
            IsFire = false;
        }

        public override void Fire()
        {
            Rigidbody bullet = Instantiate(_bullet, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
            SetBulletDamage(_bullet.GetComponent<Ammunition>());
            bullet.AddForce(bullet.transform.forward * _force, ForceMode.Impulse);

            Debug.Log("Gun fire");
            IsFire = false;
        }

        public override void MultipleTimeFire(Transform playerTransform)
        {

        }
    }
}