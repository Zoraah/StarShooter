using StarShooter.WeaponsAndAmmunitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StarShooter.Logic.Enemy
{
	public class EnemyHumanoid : Enemy
	{
		[SerializeField] private NavMeshAgent _navMeshAgent = default;
		[SerializeField] private Transform _startPosition = default;

		private Coroutine _shootCoroutine = default;
		private Coroutine _findPlayerCoroutine = default;
		private Coroutine _rotateCoroutine = default;

		[SerializeField] private float _anglesOfView = 70f;
		private bool _playerFinded = default;

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				_healthIndentificator.SetActiveHealthIndetifier(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				_playerFinded = false;
				_healthIndentificator.SetActiveHealthIndetifier(false);
				KillCoroutine(_shootCoroutine);
				_navMeshAgent.destination = other.transform.position;
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				if (!_playerFinded)
				{
					Vector3 directionToPlayer = other.transform.position - transform.position;
					float anglesToPlayer = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(directionToPlayer));
					Debug.Log(anglesToPlayer);
					if (anglesToPlayer < _anglesOfView)
					{
						_playerFinded = true;
						_shootCoroutine = StartCoroutine(Shoot(other.gameObject));
					}
				}
			}
		}

		public override void Fire()
		{
			_gun.Fire();
		}

		private IEnumerator Shoot(GameObject player)
		{
			while (_playerFinded)
			{
				Vector3 direction = player.transform.position - transform.position;

				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 2f);

				_fireDelay -= Time.deltaTime;

				if (_fireDelay <= 0)
				{
					_gun.MultipleTimeFire(player.transform);
					_fireDelay = _defaultFireDelay;
				}

				yield return null;
			}

		}
	}
}