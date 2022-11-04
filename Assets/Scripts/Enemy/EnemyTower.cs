using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarShooter.UI;

namespace StarShooter.Logic.Enemy
{
	public class EnemyTower : Enemy
	{
		private Coroutine _patrolCoroutine = default;
		private Coroutine _playerTrackingCoroutine = default;

		[SerializeField] private float _rotationSpeed = default;
		private bool _playerTracking = false;

		protected override void Awake()
		{
			_fireDelay = _defaultFireDelay;
			base.Awake();
		}

		private void Start()
		{
			_healthIndentificator.SetHealth(_defaultHealth, _health);
			_patrolCoroutine = StartCoroutine(Patrol());
		}

		private void OnTriggerEnter(Collider other)
		{
			LookAtPlayer(other.gameObject, true);
		}

		private void OnTriggerExit(Collider other)
		{
			LookAtPlayer(other.gameObject, false);
		}

		public override void Fire()
		{
			_fireDelay -= Time.deltaTime;
			if (_fireDelay <= 0)
			{
				_gun.Fire();
				_fireDelay = _defaultFireDelay;
			}
		}

		private void LookAtPlayer(GameObject gameObject, bool isLook)
		{
			if (gameObject.tag == "Player")
			{
				if(isLook)
				{
					KillCoroutine(_patrolCoroutine);
					_playerTracking = true;
					_playerTrackingCoroutine = StartCoroutine(TrackPlayer(gameObject.transform));
				}
				else
				{
					_playerTracking = false;
					KillCoroutine(_playerTrackingCoroutine);
					_patrolCoroutine = StartCoroutine(Patrol());
				}
			}
		}

		private IEnumerator TrackPlayer(Transform playerTransform)
		{
			_healthIndentificator.SetActiveHealthIndetifier(true);

			while (_playerTracking)
			{
				_gun.transform.rotation = Quaternion.RotateTowards(_gun.transform.rotation, Quaternion.LookRotation(playerTransform.position - _gun.transform.position, Vector3.up), _rotationSpeed);
				Fire();
				yield return null;
			}
		}

		private IEnumerator Patrol()
		{
			_healthIndentificator.SetActiveHealthIndetifier(false);

			Vector3 resetDirectionValues = _gun.transform.eulerAngles;
			resetDirectionValues.x = 0;

			_fireDelay = _defaultFireDelay;

			while (Mathf.Abs(_gun.transform.eulerAngles.x) > .2f)
			{
				_gun.transform.rotation = Quaternion.RotateTowards(_gun.transform.rotation, Quaternion.Euler(resetDirectionValues), _rotationSpeed);
				yield return null;
			}
			while (!_playerTracking)
			{
				_gun.transform.Rotate(0f, _rotationSpeed/2, 0f);
				yield return null;
			}
		}
    }
}