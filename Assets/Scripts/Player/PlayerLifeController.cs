using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarShooter.Interfaces;

namespace StarShooter.Logic.Player
{
    public class PlayerLifeController : MonoBehaviour , IDamagebleObject
    {
        [SerializeField] private int _health = 100;
        [SerializeField] private float _energy = 200;

        private float _timeToRestoreEnergy = 4f;
        private bool _restoreEnergy = false;

        private Coroutine _prepareToRestoreEnergyCoroutine = default;
        private Coroutine _restoreEnergyCoroutine = default;

        public void TakeDamage(int damage)
        {
            if ((int)_energy > damage)
            {
                UseEnergy((float)damage);
            }
            else if (_energy > 5)
            {
                int restOfDamage = damage - (int)_energy;
                _energy = 0;
                _health -= restOfDamage;
            }
            else
            {
                _health -= damage;
            }
        }
        
        public void UseEnergy(float energyCost)
		{
            KillCoroutine(_prepareToRestoreEnergyCoroutine);
            KillCoroutine(_restoreEnergyCoroutine);
            _energy -= energyCost;
            _prepareToRestoreEnergyCoroutine = StartCoroutine(PreparationToRestoreEnergy());
		}

        private IEnumerator PreparationToRestoreEnergy()
		{
            yield return new WaitForSeconds(_timeToRestoreEnergy);
            _restoreEnergyCoroutine = StartCoroutine(RestoreEnergy());
		}

        private IEnumerator RestoreEnergy()
		{
            while(_energy < 200)
			{
                _energy += 0.1f;
                yield return null;
			}

            _energy = 200f;
		}

        private void KillCoroutine(Coroutine coroutine)
		{
            if(coroutine != null)
			{
                StopCoroutine(coroutine);
                coroutine = null;
			}
		}
    }
}