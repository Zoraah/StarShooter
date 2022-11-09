using StarShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.WeaponsAndAmmunitions
{
	public class Ammunition : MonoBehaviour
	{
		[SerializeField] private int _damage = default;

		private bool _collided = false;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.tag != gameObject.tag)
			{
				if (collision.gameObject.GetComponent<IDamagebleObject>() != null)
				{
					collision.gameObject.GetComponent<IDamagebleObject>().TakeDamage(_damage);
					Destroy(this.gameObject);
				}
			}
		}

		public void SetBulletDamage(int damage)
		{
			_damage = damage;
		}
	}
}