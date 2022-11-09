using StarShooter.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StarShooter.Logic
{
	public class CollisionEventsController : MonoBehaviour
	{
		[SerializeField] private CollisionEvent[] _collisionEnterEvents = default;
		[SerializeField] private CollisionEvent[] _collisionExitEvents = default;
		
		private int _eventIndex = default;

		private void OnCollisionEnter(Collision other)
		{
			Relate(other.gameObject, _collisionEnterEvents);
		}

		private void OnCollisionExit(Collision other) 
		{
			Relate(other.gameObject, _collisionExitEvents);
		}

		private void Relate(GameObject gameObject, CollisionEvent[] collisionEvents)
		{
			foreach (var collisionEvent in collisionEvents)
			{
				if(gameObject.tag == collisionEvent.ObjectTag)
				{
					collisionEvent.OnCollisionEvent?.Invoke();
				}
			}
		}
	}

    [Serializable]
    internal struct CollisionEvent
	{
		public string ObjectTag;
		public UnityEvent OnCollisionEvent;
	}
}