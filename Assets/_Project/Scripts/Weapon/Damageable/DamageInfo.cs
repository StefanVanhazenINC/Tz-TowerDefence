using UnityEngine;

namespace _Project.Scripts.Weapon
{
    public struct DamageInfo
    {
        public int damage;
        public Vector3? position;
        public Vector3 Direction;
        public DamageInfo(int damage,Vector3 direction,Vector3? point = null)
        {
            this.damage = damage;
            Direction = direction;
            position = point;
        }
    }
}