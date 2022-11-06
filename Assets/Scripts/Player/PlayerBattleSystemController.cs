using StarShooter.WeaponsAndAmmunitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarShooter.Logic.Player
{
    public class PlayerBattleSystemController : MonoBehaviour
    {
        [SerializeField] private Camera _camera = default;
        [SerializeField] private Gun[] _guns = default;

        [SerializeField] protected const string SHOW_GUN_ANIMATION = "Animation_Showing";
        [SerializeField] protected const string HIDE_GUN_ANIMATION = "Animation_Hiding";

        [SerializeField] private float _rayDistance = 50f;
        [SerializeField] private int _choosedGunIndex = 0;
        private bool _canSwitchGun = true;

        private Vector3 _dir = default;

		private void Awake()
		{
            foreach(var gun in _guns)
			{
                gun.OnHideEvent.AddListener(EnableChoosedWeapon);
			}
		}

		private void Start()
		{
            EnableChoosedWeapon();
		}

		private void Update()
		{
            Debug.DrawRay(_camera.transform.position, _dir, Color.red);
        }

		public void Fire(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.ReadValueAsButton())
            {
                SetDirectionAndFire();
            }
        }

        public void SetDirectionAndFire()
		{
            RaycastHit[] raycastHit;

            Vector3 direction = default;

            raycastHit = Physics.RaycastAll(_camera.transform.position, _camera.transform.forward, _rayDistance);
			if (raycastHit.Length > 0)
            {
                foreach (var hit in raycastHit)
                {
                    if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
                    {
                        direction = hit.point - _guns[_choosedGunIndex].BulletSpawnerPosition.position;
                        _guns[_choosedGunIndex].Fire(Quaternion.LookRotation(direction, Vector3.up));
                        break;
                    }
                }
            }
            else
			{
                direction = (_camera.transform.position + _camera.transform.forward * _rayDistance)  - _guns[_choosedGunIndex].BulletSpawnerPosition.position;
                _guns[_choosedGunIndex].Fire(Quaternion.LookRotation(direction, Vector3.up));
            }
            _dir = direction;
		}

		#region WeaponsControlling

		public void ChooseWeapon(InputAction.CallbackContext callbackContext)
        {
            float scrollValue = callbackContext.ReadValue<float>();
            Debug.Log(scrollValue.ToString());
            if (_canSwitchGun)
            {
                if (scrollValue > 0.1f)
                {
                    SelectPreviousWeapon();
                }
                else if (scrollValue < -0.1f)
                {
                    SelectNextWeapon();
                }
            }
		}

        private void SelectPreviousWeapon()
        {
            if (_choosedGunIndex != 0)
            {
                HideWeapon(_choosedGunIndex);
                _choosedGunIndex--;
            }
        }

        private void SelectNextWeapon()
        {
            if (_choosedGunIndex < _guns.Length - 1)
            {
                HideWeapon(_choosedGunIndex);
                _choosedGunIndex++;
            }
        }

        private void HideWeapon(int weaponIndex)
		{
            _canSwitchGun = false;
            _guns[weaponIndex].PlayAnimation(HIDE_GUN_ANIMATION);
        }

        private void EnableChoosedWeapon()
		{
            _guns[_choosedGunIndex].gameObject.SetActive(true);
            _guns[_choosedGunIndex].PlayAnimation(SHOW_GUN_ANIMATION);
            _canSwitchGun = true;
        }

		#endregion
	}
}