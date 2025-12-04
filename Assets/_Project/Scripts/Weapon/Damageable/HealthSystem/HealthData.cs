using System;
using Alchemy.Inspector;
using UnityEngine;

namespace _Project.Scripts.Weapon.HealthSystem
{
    public class HealthData: IDisposable   , ICloneable
    {
        [SerializeField] private string _descriptionTarget;
        [SerializeField] private int _maxHealth;
       
        [SerializeField,ReadOnly]private int _health;
        
        public event Action OnHealthChanged = delegate { };
        public event Action OnDeath = delegate { };
        public event Action OnDeathOnePact = delegate { };
        public event Action OnInitHealth  = delegate { };
        public int MaxHealth => _maxHealth; 
        public int Health
        {   
            get => _health; 
            set
            {
            
                _health = value;    
                if (_health <0)
                {
                    _health = 0;
                }

                if (_health > _maxHealth)
                {
                    _health = _maxHealth;
                }

                OnHealthChanged?.Invoke();
                if (IsDeath )
                {
                    OnDeath?.Invoke();

                    if (OnDeathOnePact != null)
                    {
                        OnDeathOnePact?.Invoke();
                        OnDeathOnePact = null;
                    }

                }
            }

        }
        public bool IsCanDamage => !IsDeath;
        public bool IsDeath  => _health <= 0;
        public bool HealthIsMax => _health >= _maxHealth;
        public string DescriptionTarget  => _descriptionTarget;

        public void ResetHealth()
        {
            Health = _maxHealth;
        }

        public void SetParametrs( int maxHealth)
        {
            _maxHealth = maxHealth;
            OnHealthChanged?.Invoke();
        }

        public void Reset()
        {
            OnHealthChanged = delegate { };
            OnDeath = delegate { };
            OnInitHealth  = delegate { };   
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public void Dispose()
        {
            Reset();
        }    
    }
}