using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarShooter.UI
{
    public class UIController : MonoBehaviour
    {
		private bool ss;

		private void Start()
		{
			DisableMouse(default);
		}

		private void Update()
		{
			if(ss)
			{
				Debug.Log("Q pressed");
			}
		}

		public void DisableMouse(InputAction.CallbackContext callbackContext)
		{
			if (callbackContext.ReadValueAsButton())
			{
				Debug.Log("Mouse hided");
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}
}