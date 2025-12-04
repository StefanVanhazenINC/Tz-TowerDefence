using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts.Weapon
{
    public class WeaponVisual : MonoBehaviour
    {
        [SerializeField] private Transform _shotDir;
        [SerializeField] private UnityEvent _onShotEvent;
        
        public Transform ShotDir => _shotDir;

        [Button]
        public void ShotAnimation()
        {
            _onShotEvent?.Invoke();
        }

    }
}