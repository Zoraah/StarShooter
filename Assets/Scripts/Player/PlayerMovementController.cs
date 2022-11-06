using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarShooter.Logic.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _playerRigidbody = default;

        [SerializeField] private float _runSpeed = default;
        [SerializeField] private float _walkSpeed = default;

        [SerializeField] private float _speedValue = default;
        [SerializeField] private float _jumpPower = default;

        private float _movementX = default;
        private float _movementY = default;

		private void Awake()
		{
            _speedValue = _walkSpeed;
		}

		private void Update()
		{
            Move();
        }

        public void GetMovementValues(InputAction.CallbackContext callbackContext)
		{
            Vector2 movementVector = callbackContext.ReadValue<Vector2>();
            _movementX = movementVector.x;
            _movementY = movementVector.y;
        }

        public void GetRunValue(InputAction.CallbackContext callbackContext)
		{
            bool isRun = callbackContext.ReadValueAsButton();
            
            if(isRun)
			{
                _speedValue = _runSpeed;
			}
            else
			{
                _speedValue = _walkSpeed;
			}
		}

        public void Jump(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.ReadValueAsButton())
            {
                _playerRigidbody.AddForce(Vector3.up * _jumpPower);

                Debug.Log("Jump");
            }
        }

        private void Move()
		{
            _playerRigidbody.position += (_playerRigidbody.transform.forward * _movementY + _playerRigidbody.transform.right * _movementX) * _speedValue * Time.deltaTime;
        }
    }
}