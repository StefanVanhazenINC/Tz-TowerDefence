using System;
using _Project.Scripts.Weapon.Base;
using UnityEngine;

namespace _Project.Scripts.Weapon.Data
{
    [Serializable]
    public class WeaponData
    {
        [Header("Visual")]
        public string Name;
        public Sprite Visual;    
        
        
        [Header("BattleParam")] 
        [SerializeReference] public IShoter ShoterPrototype;
        
        public int Damage;
        public int Range;
        public float DelayShot;
        public float BulletSpeed;
        public WeaponTypeShot WeaponTypeShot;
    }
}