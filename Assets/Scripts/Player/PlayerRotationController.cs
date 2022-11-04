using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotationController : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidody = default;
	[SerializeField] private Transform _playerCamera = default;

    [SerializeField] private float _rotationSpeed = default;

    private float _rotateValueX = default;
    private float _rotateValueY = default;

	private float _rotationY = default;
	private float _rotationX = default;

	private void Start()
	{
		
	}

	private void Update()
	{
		RotateBodyAndCamera();
	}

	public void GetRotationValues(InputAction.CallbackContext callbackContext)
	{
		Vector2 rotationValues = callbackContext.ReadValue<Vector2>();

		_rotateValueX = rotationValues.x;
		_rotateValueY = rotationValues.y;
	}

	private void RotateBodyAndCamera()
	{
		_rotationY += -_rotateValueY * _rotationSpeed * Time.deltaTime;
		_rotationY = Mathf.Clamp(_rotationY, -40f, 40f);
		_rotationX += _rotateValueX * _rotationSpeed * Time.deltaTime;

		_playerCamera.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
		_playerRigidody.rotation = Quaternion.Euler(0f, _rotationX, 0f);
	}
}
