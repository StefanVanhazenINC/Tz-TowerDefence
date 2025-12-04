using System;
using _Project.Scripts.Ai;
using _Project.Scripts.Targetable;
using Alchemy.Inspector;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class EnemyFacade : MonoBehaviour
    {
        [SerializeField] private float _speed =5 ;
        [SerializeField] private int _health = 10;
        [SerializeField] private int _coin = 1;
        [SerializeField] private Targetable2D _targetable;
        [SerializeField] private Rigidbody2D _rb;   
            
        private MoveTo _moveTo;

        public bool IsDeath => !_targetable.IsAlive;
        public int Coin => _coin;
        
        public event Action<EnemyFacade> OnDeath = delegate { }; 
        
        public void Create()
        {
            if ( _moveTo == null)
            {
                _moveTo = new MoveTo(_rb, _speed);
            }
            _targetable.Construct(_health);
            _targetable._healthSystem.OnDeath += DeathEvent;   
        }

        public void FixedUpdate()
        {
            if (_moveTo != null)
            {
                _moveTo.FixedTick();    
            }
        }
        [Button]
        public void DeathEvent()
        {
            OnDeath?.Invoke(this);    
        }

        private void OnDisable()
        {
            if (_targetable._healthSystem!=null)
            {
                _targetable._healthSystem.OnDeath -= DeathEvent;   
            }
        }
    }
}