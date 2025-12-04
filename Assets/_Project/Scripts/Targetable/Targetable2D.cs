using System;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.HealthSystem;
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _Project.Scripts.Targetable
{
    public class Targetable2D : MonoBehaviour  , IDisposable
    {
        [SerializeField]private Collider2D _col;
        
        public HealthSystem _healthSystem;
        public HealthData _healthData;
        public UnityEvent OnTakeDamage = new UnityEvent();
        public UnityEvent OnDeath = new UnityEvent();
        

        [Button]
        public void Construct(int maxHp)
        {
            if (_healthData==null)
            {
                _healthData = new HealthData();
            }
            _healthData.SetParametrs(maxHp);
            _healthData.ResetHealth();
            if (_healthSystem==null)
            {
                _healthSystem = new HealthSystem(_healthData);
            }

            _healthSystem.OnDeath += () =>  OnDeath?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            _healthSystem.TakeDamage(new DamageInfo(damage,Vector3.zero,Vector3.zero));  
            OnTakeDamage?.Invoke();
        }
        public void TakeDamage(DamageInfo damage)
        {
            _healthSystem.TakeDamage(damage);
            OnTakeDamage?.Invoke();
        }
        
        public int MaxHP => _healthSystem.MaxHealth;
        public int CurrentHP => _healthSystem.Health;
        public bool IsAlive => !_healthSystem.IsDeath;
        public Vector3 AimPoint => _col ? _col.bounds.center : transform.position;//выдать случайную точку внути bounds 

        public void Dispose()
        {
            _healthSystem?.Dispose();
            _healthData?.Dispose();
        }
    }
}