using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StarShooter.Interfaces;

namespace StarShooter.Logic
{
    public class LiveObjectCollisionController : MonoBehaviour , IDamagebleObject
    {
		public UnityEvent<int> OnDamageTakedEvent = default;

		public void TakeDamage(int damage)
		{
			OnDamageTakedEvent?.Invoke(damage);
		}
	}
}