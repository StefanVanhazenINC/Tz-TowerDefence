using _Project.Scripts.Targetable;
using UnityEngine;

namespace _Project.Scripts.Weapon.Data
{
    public class WeaponUseContext
    {
        public bool IsPlace ;
        public Targetable2D Target;
        public Vector2 Place;
        public WeaponUseContext(bool isPlace, Vector2 place, Targetable2D target)
        {
            IsPlace = isPlace;
            Place = place;
            Target = target;     
        }
    }
}