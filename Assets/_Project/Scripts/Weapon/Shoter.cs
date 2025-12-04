using System;
using _Project.Scripts.Weapon.Base;
using _Project.Scripts.Weapon.Data;
using _Project.Scripts.Weapon.ProjectileBase;
using UnityEngine;

namespace _Project.Scripts.Weapon
{
    [Serializable]
    public class Shoter: IShoter
    {
        [SerializeField] private Projectile _projectilePrefab;

        [SerializeField] private float _projectileMaxHeight;

        [SerializeField] private AnimationCurve _trajectoryAnimationCurve;
        [SerializeField] private AnimationCurve _axisCorrectionAnimationCurve;
        [SerializeField] private AnimationCurve _projectileSpeedAnimationCurve;
        private IPoolProjectile _pool;
        public Shoter() { }
        public Shoter(Projectile prefab, float projectileMaxHeight, AnimationCurve trajectoryAnimationCurve,
            AnimationCurve axisCorrectionAnimationCurve, AnimationCurve projectileSpeedAnimationCurve, IPoolProjectile pool)
        {
            _projectilePrefab = prefab;
            _projectileMaxHeight = projectileMaxHeight;
            _trajectoryAnimationCurve = trajectoryAnimationCurve;
            _axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
            _projectileSpeedAnimationCurve = projectileSpeedAnimationCurve;
            _pool = pool;
        }    
        
        private Shoter(Shoter other, IPoolProjectile pool)
        {
            _projectilePrefab = other._projectilePrefab;
            _projectileMaxHeight = other._projectileMaxHeight;
            _trajectoryAnimationCurve = other._trajectoryAnimationCurve;
            _axisCorrectionAnimationCurve = other._axisCorrectionAnimationCurve;
            _projectileSpeedAnimationCurve = other._projectileSpeedAnimationCurve;
            _pool = pool;
        }
        
        public void Shot(WeaponContext context)
        {
            Projectile projectile = _pool.GetObjectInPool(_projectilePrefab) as Projectile;
            projectile.SetProjectile(context.DamageInfo);
            projectile.SetPositionAndRotation(context.ShotDir.position, Quaternion.identity);
            projectile.InitializeAnimationCurves(_trajectoryAnimationCurve, _axisCorrectionAnimationCurve,
                _projectileSpeedAnimationCurve);
            if (context.IsPlace)
            {
                projectile.InitializeProjectile(context.Place,  context.MoveSpeed, _projectileMaxHeight);
            }
            else
            {
                projectile.InitializeProjectile(context.Target, context.MoveSpeed, _projectileMaxHeight);
            }
        }

        public IShoter Clone(IPoolProjectile pool)
        {
            return new Shoter(this, pool);
        }
    }
}