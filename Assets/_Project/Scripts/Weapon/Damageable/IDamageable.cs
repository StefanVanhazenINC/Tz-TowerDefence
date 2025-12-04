namespace _Project.Scripts.Weapon
{
 
    public interface IDamageable
    {
        public bool DontDestroyBullet { get; }
        public bool IsCanDamage { get;  }
        public void TakeDamage(DamageInfo info);
    }
}