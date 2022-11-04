using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigidbody = default;

    [SerializeField] private float _defaultDelay = default;
    [SerializeField] private float _delay = default;
	[SerializeField] private float _force = default;

	private void Start()
	{
		_delay = _defaultDelay;
	}

	private void Update()
	{
		_delay -= Time.deltaTime;

		if (_delay <= 0)
		{
			Rigidbody _rbObject = Instantiate(_bulletRigidbody) as Rigidbody;
			_rbObject.position = transform.position;
			_rbObject.AddForce(Vector3.forward * _force, ForceMode.Impulse);
			_delay = _defaultDelay;
		}
	}
}
