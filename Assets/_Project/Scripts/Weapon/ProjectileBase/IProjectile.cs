using System;
using UnityEngine;

namespace _Project.Scripts.Weapon.ProjectileBase
{
    public interface IProjectile
    {
        public event Action<IProjectile> DisableCallback;
        public void SetProjectile(BulletInfo info);
        public void SetPositionAndRotation(Vector3 position,Quaternion rotation);
        public void Disable();
        public void Active(); 
    }
}