using System.Collections.Generic;
using _Project.Scripts.Targetable;
using UnityEngine;

namespace _Project.Scripts.Weapon.Data
{
    public struct WeaponContext
    {
        public bool IsPlace ;
        public Targetable2D Target;
        public Vector2 Place;
        public DamageInfo DamageInfo;
        public float MoveSpeed;
        public Transform ShotDir; 
    }
}