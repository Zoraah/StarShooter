using StarShooter.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StarShooter.Logic
{
	public class CollisionEventsController : MonoBehaviour, ICollisionRelationObject
	{
		[SerializeField] private CollisionEvent[] _collisionEnterEvents = default;

		private int _eventIndex = default;

		private void OnCollisionEnter(Collision collision)
		{
			RelateWithObject(collision.gameObject);
		}

		public void DoCollisionOperation(int value)
		{
			_collisionEnterEvents[_eventIndex].OnCollisionEvent?.Invoke(value);
		}

		private void RelateWithObject(GameObject obj)
		{
			ICollisionObject collisionObject = obj.GetComponent<ICollisionObject>();

			if (collisionObject != null)
			{
				for (int i = 0; i < _collisionEnterEvents.Length; i++)
				{
					if (_collisionEnterEvents[i].ObjectTag == obj.tag)
					{
						_eventIndex = i;
						collisionObject.DoCollisionOperation(this.gameObject);
					}
				}
			}

			_eventIndex = 0;
		}
	}

    [Serializable]
    internal struct CollisionEvent
	{
		public string ObjectTag;
		public UnityEvent<int> OnCollisionEvent;
	}
}