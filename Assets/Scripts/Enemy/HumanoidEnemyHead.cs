using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HumanoidEnemyHead : MonoBehaviour
{
	public UnityEvent<Vector3> OnPlayerDetected = default;
	public UnityEvent<Vector3> OnPlayerLosted = default;

	private Vector3 _playerPosition = default;

	[SerializeField] private float _anglesOfView = 70f;
	private float _anglesToPlayer = default;
	private bool _isPlayerFinded = default;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			CheckPlayerInVision(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			DoPlayerLostOperations();
		}
	}

	private void DoPlayerLostOperations()
	{
		_isPlayerFinded = false;
		OnPlayerLosted?.Invoke(_playerPosition);
	}

	private void CheckPlayerInVision(Collider other)
	{
		_playerPosition = other.transform.position;
		Vector3 directionToPlayer = _playerPosition - transform.position;
		_anglesToPlayer = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(directionToPlayer));

		if (_anglesToPlayer < _anglesOfView)
		{
			_isPlayerFinded = true;
			OnPlayerDetected?.Invoke(_playerPosition);
		}
	}
}
