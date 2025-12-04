using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _Project.Scripts.Weapon.HealthSystem
{
    public class HealthSystem: IDamageable,IDisposable
    {
        private readonly HealthData _data;
        public  DamageInfo LastDamageInfo;
        public bool IsDeath =>  _data.IsDeath;
        public UnityEvent<DamageInfo> OnSendDamageInfoUnity;
        public event Action<DamageInfo> OnSendDamageInfo = delegate { };
        public event Action OnTakeDamage = delegate { };
        public event Action OnDeathDamaged = delegate { };

        public int Health
        {
            get  => _data.Health;
            set => _data.Health = value;
        }
        public int MaxHealth => _data.MaxHealth;
        public event Action OnDeath = delegate { };
        [Inject]
        public HealthSystem(HealthData data) 
        {
            _data = data;
            ResetHealth();
            _data.OnDeath += Death;
        }
      
        private void Death()
        {
            Debug.Log("DeathSystem");
            OnDeath?.Invoke();     
        }

        public void ResetHealth()
        {
            _data.ResetHealth();
        }

        public bool DontDestroyBullet { get => true; }
        public bool IsCanDamage => _data.IsCanDamage;

        public void TakeHealth(int amount)
        {
            if (!IsDeath)
            {
                Health += amount;
            }
        }

        public void ChangeMaxHealth(int amount)
        {
            _data.SetParametrs(amount);
        }

        public void TakeDamage(DamageInfo info)
        {
            if (!IsDeath && info.damage > 0 ) 
            {
                OnTakeDamage?.Invoke();
                OnSendDamageInfo?.Invoke(info);
                OnSendDamageInfoUnity?.Invoke(info);
                if ( Health - info.damage<=0)
                {
                    OnDeathDamaged?.Invoke();   
                }
                Health -= info.damage;
                LastDamageInfo = info;  
             

                
            }
        }

        public void Dispose()
        {
            _data.OnDeath -= OnDeath;
            OnDeath = null;
            OnSendDamageInfo = null;
            OnTakeDamage = null;
            OnDeathDamaged = null;
            _data?.Dispose();
            
        }

      
    }
}