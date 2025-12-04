using _Project.Scripts.Ai;
using _Project.Scripts.Targetable;
using _Project.Scripts.Tower.Data;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Data;
using _Project.Scripts.Weapon.Factory;
using Alchemy.Inspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Tower
{
    public class TowerFacade : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private TowerClass _towerClass;
        
        [Header("Component")]
        [SerializeField] private AreaTargetingSystem _targetSystem;
        [SerializeField] private SpriteRenderer _visual;
        
        [Header("Settings-Aim")]
        [SerializeField] private float _rotationSpeed = 100f;
        [SerializeField] private Transform _armPivot;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private float _coneAngle = 45f;


        private WeaponConfig _baseWeaponConfig;
        private BaseWeapon _weapon;
        
        private float _lastTimeShot = -100;
        
        private Targetable2D _target;
        private Aimming _aimming;
        private IWeaponFactory _weaponFactory;
        private TowerConfig _config; 
        
        public  bool TargetNotNull=> _target != null;
        public  bool CheckReadyWeapon => Time.time >= _lastTimeShot + _weapon.DelayShot && CanShotAngle();
        public bool WeaponNull => _weapon == null;
        
        [Inject]
        private void Construct(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }
        
        [Button]
        public void Create(TowerConfig config)
        {
            _towerClass = config.TowerClass;
            _config = config;
            Create();
        }
        public void Create()
        {
            if (_aimming==null)
            {
                _aimming = new Aimming(_armPivot,new Transform[]{_weaponHolder},_rotationSpeed);
            }
            _targetSystem.OnTargetChanged += ChangeTarget;
            _lastTimeShot = Time.time;
            _baseWeaponConfig = _towerClass.BaseWeapon;
            _visual.sprite = _towerClass.Visual;
            SetWeapon(_baseWeaponConfig);
        }

        public void AimToTarget()
        {
            _aimming.Aim(_target.AimPoint);
        }
        public void UseWeapon()
        {
            _lastTimeShot = Time.time;
            _weapon.UseWeapon(new WeaponUseContext(false,Vector2.zero,_target));  
        }
        
        public bool CanShotAngle()
        {
            if (_target )
            {
                Vector2 directionToTarget = (_target.AimPoint - _armPivot.position).normalized;
                Vector2 forwardDirection = _armPivot.right; 
                float dotProduct = Vector2.Dot(forwardDirection, directionToTarget);
                float cosConeAngle = Mathf.Cos(_coneAngle * 0.5f * Mathf.Deg2Rad);
                return dotProduct >= cosConeAngle ;
            }
            return false;
        }

        private void SetWeapon(WeaponConfig config)
        {
            WeaponConfig lastWeapon = null;
            if (_weapon !=null)
            {
                Destroy(_weapon.gameObject);
            }
         
            _weapon = _weaponFactory.Create(config);
            _weapon.SetupPosition(_weaponHolder);
            SetRangeTower(_weapon.Range);
        }

        private void SetRangeTower(float range)
        {
            _targetSystem.SetRange(range);
        }
        private void ChangeTarget(Targetable2D target)
        {
            _target = target;     
        }

        private void OnDrawGizmosSelected()
        {
            if (_weapon)
            {
                Gizmos.DrawWireSphere(transform.position,_weapon.Range);
            }
        }
    }
}