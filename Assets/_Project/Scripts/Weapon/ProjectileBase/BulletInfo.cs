using UnityEngine;

namespace _Project.Scripts.Weapon.ProjectileBase
{
    public struct BulletInfo
    {
        public Vector3 direction;
        public int damage;
        public float speed;
        public GameObject visual;
        public BulletInfo(Vector3 direction, int damage,float force, float speed, float lifeTime,GameObject visual = null)
        {
            this.direction = direction;
            this.damage = damage;
            this.speed = speed;
            this.visual = visual;
        }
    }
}