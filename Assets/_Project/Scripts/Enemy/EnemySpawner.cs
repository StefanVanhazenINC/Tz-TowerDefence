using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Vector2 _sizeSpawner = Vector2.one;
        [SerializeField] private float _spawnInterval = 0.5f;

        private Vector2 _workSapce;
        
        [Inject]
        private EnemyPool _pool;
        private float _spawnTimer;
        public void Update()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (Time.time >= _spawnTimer +  _spawnInterval )
            {
                _spawnTimer = Time.time;
                _pool.Spawn(RandomPosition());
            }
        }

        private Vector2 RandomPosition()
        {
            float x = Random.Range(-_sizeSpawner.x, _sizeSpawner.x);
            float y = Random.Range(-_sizeSpawner.y, _sizeSpawner.y);
            
            _workSapce.Set(transform.position.x + x, transform.position.y + y);
            return _workSapce;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, _sizeSpawner);
        }
    }
}