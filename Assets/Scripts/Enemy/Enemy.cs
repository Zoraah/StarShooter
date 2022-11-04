using StarShooter.WeaponsAndAmmunitions;
using StarShooter.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.Logic.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected HealthIndentificator _healthIndentificator = default;
        [SerializeField] protected Gun _gun = default;

        [SerializeField] protected int _defaultHealth = default;
        [SerializeField] protected int _health = default;

        [SerializeField] protected float _defaultFireDelay = default;
        [SerializeField] protected float _fireDelay = default;

        protected virtual void Awake()
        {
            _health = _defaultHealth;
        }

        public abstract void Fire();

        public virtual void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Destroy(this.gameObject);
            }

            _healthIndentificator.SetHealth(_defaultHealth, _health);
        }

        protected void KillCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}