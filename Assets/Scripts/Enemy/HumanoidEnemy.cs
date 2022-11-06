using StarShooter.WeaponsAndAmmunitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StarShooter.Logic.Enemy
{
	public class HumanoidEnemy : Enemy
	{
		[SerializeField] private HumanoidEnemyHead _head = default;

		[SerializeField] private NavMeshAgent _navMeshAgent = default;
		[SerializeField] private Transform _startPosition = default;

		private Coroutine _rotateAndShootCoroutine = default;

		private void Start()
		{
			_head.OnPlayerDetected.AddListener(RotateAndShoot);
			_head.OnPlayerLosted.AddListener(MoveTo);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				SetActiveHealthIndentificator(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.tag == "Player")
			{
				SetActiveHealthIndentificator(false);
			}
		}

		public override void Fire()
		{
			_gun.Fire();
		}

		private void SetActiveHealthIndentificator(bool isActive)
		{
			_healthIndentificator.SetActiveHealthIndetifier(isActive);
		}

		private void RotateAndShoot(Vector3 playerPosition)
		{
			SetActiveHealthIndentificator(true);
			StopMoving();
			Vector3 direction = playerPosition - transform.position;

			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 2f);
			float anglesToPlayer = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));

			_fireDelay -= Time.deltaTime;

			if (_fireDelay <= 0)
			{
				if (anglesToPlayer < 10f)
				{
					Fire();
					_fireDelay = _defaultFireDelay;
				}
			}
		}

		private void MoveTo(Vector3 playerPosition)
		{
			_navMeshAgent.isStopped = false;
			_navMeshAgent.SetDestination(playerPosition);
		}

		private void StopMoving()
		{
			_navMeshAgent.isStopped = true;
		}
	}
}