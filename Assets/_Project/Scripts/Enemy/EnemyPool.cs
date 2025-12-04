using System;
using _Project.Scripts.SignalBusAndSignal;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemy
{
    public class EnemyPool: MonoMemoryPool<Vector3,EnemyFacade>
    {
        public event Action OnEnemyDeath = delegate { };

        [Inject]
        private SignalBus  _scoreBus;
        protected override void Reinitialize(Vector3 pos, EnemyFacade item)
        {
            item.Create();
            item.transform.position = pos;
            item.gameObject.SetActive(true);
            base.Reinitialize(pos, item);
        }
        protected override void OnCreated(EnemyFacade item)
        {
            item.OnDeath += Despawn;
            item.gameObject.SetActive(false);

        }

        protected override void OnDespawned(EnemyFacade item)
        {
            OnEnemyDeath?.Invoke();
            Debug.Log(item.IsDeath);
            if (item.IsDeath)
            {
            
                AddScore(item);
            }
            item.gameObject.SetActive(false);  
        }

        //ToDo: по хорошемоу надо сделать каталог врагов , чтоб их стоимость была прописана там, а не в фасаде 
        private void AddScore(EnemyFacade item)
        {
            _scoreBus.FireId(SignalID.ADD_COIN , item.Coin);    
        }
    }
}