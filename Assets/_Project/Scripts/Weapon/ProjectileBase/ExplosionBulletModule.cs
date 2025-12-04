using _Project.Scripts.Targetable;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Weapon.ProjectileBase
{
    public class ExplosionBulletModule : MonoBehaviour
    {
        [SerializeField] private Projectile _reference;
        [SerializeField] private float _rangeExplosion = 1f;
        [SerializeField] private LayerMask _layerTarget;
        [SerializeField] private float _disableAfterExplosion = 0.2f;
        [SerializeField] private ParticleSystem _particleSystem;
        
        public void ExplosionImidiatly()
        {
            _particleSystem.Play();
            FindTargets();
            TimerToDisable().Forget();     
        }
        private void FindTargets()
        {
            Collider2D[] tempTarget = Physics2D.OverlapCircleAll(transform.position,_rangeExplosion, _layerTarget);
            for (int i = 0; i <  tempTarget.Length; i++)
            {
                if (tempTarget[i] !=null  && tempTarget[i].TryGetComponent(out Targetable2D target))
                {
                    target.TakeDamage(_reference.DamageInfo);  
                }    
            }
           
        }
        private async UniTaskVoid TimerToDisable()
        {
            //_particle.SetTrigger("Activate");
            await UniTask.WaitForSeconds(_disableAfterExplosion);
            _reference.DisableFromOutside();

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,_rangeExplosion);
        }
    }
}