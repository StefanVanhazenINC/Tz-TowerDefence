using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Weapon.ProjectileBase
{
    public class ProjectilePool : IPoolProjectile
    {
        private int _maxSizePool ;
        private Transform _parentPool;

        private readonly Dictionary<Projectile, ObjectPool<IProjectile>> _pools = new();
        private readonly Dictionary<IProjectile, Projectile> _objectToPrefabMap = new();
        
        public ProjectilePool(Transform parentPool, int maxSizePool = 10000)
        {
            _maxSizePool = maxSizePool;
            _parentPool = parentPool;
        }
        
        private ObjectPool<IProjectile> CreateNewPool(Projectile prefab)
        {
            return new ObjectPool<IProjectile>(
                () => CreatePooledObject(prefab),
                OnTakeFromPool,
                OnReturnToPool,
                OnDestroyObject,
                true,
                20,
                _maxSizePool
            );
        }

        private void OnDestroyObject(IProjectile obj)
        {
            Projectile destroyObj = obj as Projectile;
            GameObject.Destroy(destroyObj.gameObject); 
        }

        private void OnReturnToPool(IProjectile obj)
        {
            
            obj.Disable();
        }

        private void OnTakeFromPool(IProjectile prefab)
        {
            // prefab.Active();
        }

        private IProjectile CreatePooledObject(Projectile prefab)
        {
            Projectile obj;

            obj = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, _parentPool) ;
        
            
            obj.gameObject.SetActive(false);
            obj.DisableCallback += ActionToReturnInPool;

            _objectToPrefabMap[obj] = prefab;
            return obj;
        }
        void ActionToReturnInPool(IProjectile obj)
        {
            if (_objectToPrefabMap.TryGetValue(obj, out var prefab) && _pools.TryGetValue(prefab, out var pool))
            {
                pool.Release(obj);
            }
        }

        public IProjectile GetObjectInPool(Projectile prefab)
        {
            if (!_pools.ContainsKey(prefab))
            {
                _pools[prefab] = CreateNewPool(prefab);
            }

            return _pools[prefab].Get();
        }
     
    }
}