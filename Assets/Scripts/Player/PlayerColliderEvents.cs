using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StarShooter.Interfaces;

namespace StarShooter.Logic.Player
{
    public class PlayerColliderEvents : MonoBehaviour , ICollisionRelationObject
    {
		public UnityEvent OnGroundCollisionEvent = default;
		
		public UnityEvent OnFallToGroundCollisionEvent = default;
		
		public UnityEvent<int> OnDamageTakedEvent = default;

		private void OnCollisionEnter(Collision collision)
		{
			
		}

		public void DoCollisionOperation(int damage)
		{
			OnDamageTakedEvent?.Invoke(damage);
		}
	}
}