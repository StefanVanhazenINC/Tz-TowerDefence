using System;
using _Project.Scripts.Weapon.Data;
using UnityEngine;

namespace _Project.Scripts.Tower.Data
{
    [Serializable]
    public class TowerClass
    {
        [Header("Visual")]
        public Sprite Visual;
        public string Name;
        
       [Header("BattleParam")]
        public WeaponConfig BaseWeapon;
    }
}