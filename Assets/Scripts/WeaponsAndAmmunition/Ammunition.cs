using StarShooter.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.WeaponsAndAmmunitions
{
	public class Ammunition : MonoBehaviour, ICollisionObject
	{
		[SerializeField] private int _damage = default;

		private bool _collided = false;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.tag != gameObject.tag)
			{
				Destroy(this.gameObject);
			}
		}

		public void DoCollisionOperation(GameObject gameObject)
		{
			ICollisionRelationObject playerRelations = gameObject.GetComponent<ICollisionRelationObject>();
			playerRelations.DoCollisionOperation(_damage);
			Destroy(this.gameObject);
		}

		public void SetBulletDamage(int damage)
		{
			_damage = damage;
		}
	}
}