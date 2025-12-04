using System;
using _Project.Scripts.Targetable;
using _Project.Scripts.Weapon.Base;
using _Project.Scripts.Weapon.Data;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.Weapon
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _config;  
        [SerializeReference] private IShoter _shooter;
        
        
        private WeaponVisual _weaponVisual;
        
        public event Action OnShot = delegate { };
        public event Action OnShotEvent = delegate { };

        public float Range => _config.WeaponData.Range;
        public float DelayShot => _config.WeaponData.DelayShot;
        private float BulletSpeed => _config.WeaponData.BulletSpeed;
        public int Damage => _config.WeaponData.Damage;
        public void Set(IShoter shoter, WeaponConfig config,WeaponVisual weaponVisual)
        {
            _shooter  = shoter;  
            _config = config;
            _weaponVisual = weaponVisual;
        }

        public void SetupPosition(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }

        public void UseWeapon(WeaponUseContext context)
        {
            if (context.IsPlace)
            {
                UseWeapon(context.Place);
            }
            else
            {
                UseWeapon(context.Target);   
            }
        }
        public void UseWeapon(Targetable2D target)
        {
            switch (_config.WeaponData.WeaponTypeShot )
            {
                case WeaponTypeShot.ToTarget:
                    DamageInfo info = CreateDamageInfo();
                    WeaponContext context = CreateContext(target,info);
                    UseWeapon(context);
                    break;
                case WeaponTypeShot.ToPlace:
                    UseWeapon(target.AimPoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void UseWeapon(Vector2 place)
        {
            DamageInfo info = CreateDamageInfo();
            WeaponContext context = CreateContext(place,info);
            UseWeapon(context); 
        }
        private void UseWeapon(WeaponContext context)
        {
            OnShot?.Invoke();
            OnShotEvent?.Invoke();
            _weaponVisual?.ShotAnimation();
            _shooter.Shot(context);     
        }
        private DamageInfo CreateDamageInfo()
        {
            DamageInfo info = new DamageInfo();
            info.damage = Damage;
            return info;
        }
        private WeaponContext CreateContext(Vector2 place,DamageInfo info)
        {
            WeaponContext context = new WeaponContext();
            context.IsPlace = true;
            context.Place = place;
            return CreateContext(context,info);
        }
        private WeaponContext CreateContext(Targetable2D target,DamageInfo info)
        {
            WeaponContext context = new WeaponContext();
            context.Target = target;
            return CreateContext(context,info);
        }
        private WeaponContext CreateContext(WeaponContext context,DamageInfo info)
        {
            context.MoveSpeed = BulletSpeed;
            context.DamageInfo = info;
            context.ShotDir = _weaponVisual.ShotDir;
            return context;
        }
      
    }
}