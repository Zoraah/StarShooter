using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StarShooter.UI
{
    public class HealthIndentificator : MonoBehaviour
    {
		private Camera _camera = default;

        [SerializeField] private Image _healthImage = default;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
            _healthImage.transform.LookAt(_camera.transform);
		}

        public void SetHealth(int defaultHealth, int currentHealth)
		{
            _healthImage.fillAmount = (float)currentHealth / (float)defaultHealth;
		}

		public void SetActiveHealthIndetifier(bool isActive)
		{
			_healthImage.gameObject.SetActive(isActive);
		}
    }
}