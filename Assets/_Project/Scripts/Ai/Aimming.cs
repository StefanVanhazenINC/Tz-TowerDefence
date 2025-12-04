using UnityEngine;

namespace _Project.Scripts.Ai
{
    public class Aimming
    {
        private Transform _armPivot;
        private Transform[] _flipTransform;
        private float _rotationSpeed;
        
        public bool CanAimming { get; set; }
        public int FacingDirection { get; private set; }

        public Aimming(Transform armPivot,Transform[] transfromToFlip, float rotationSpeed)
        {
            CanAimming = true;
            FacingDirection = 1;
            _armPivot = armPivot;
            _flipTransform = transfromToFlip;
            _rotationSpeed = rotationSpeed;
        }
        public void Aim(Vector2 target) 
        {
            if (CanAimming)
            {
                Vector2 directioMouseLook =(Vector2)_armPivot.position - target;
                float angle = Mathf.Atan2(directioMouseLook.y, directioMouseLook.x) * Mathf.Rad2Deg;
                angle+=180;
                CheckIfShouldFlip(target.x, _armPivot.position.x);
                Quaternion lerpAngle = Quaternion.Lerp(_armPivot.rotation, Quaternion.Euler(0, _armPivot.rotation.y, angle), Time.deltaTime * _rotationSpeed);
                _armPivot.localRotation = lerpAngle;
            }                                                              
        }
        
        public void CheckIfShouldFlip(float targetX,float pivotX) 
        {
            if (targetX > pivotX)
            {
                FacingDirection = 1;
            }
            else
            {
                FacingDirection = -1;
            }
            for (int i = 0; i < _flipTransform.Length; i++)
            {
                if (FacingDirection == 1)
                {
                    _flipTransform[i].localRotation = Quaternion.Euler(0, 0, 0);

                }
                else
                {
                    _flipTransform[i].localRotation = Quaternion.Euler(180, 0, 0);
                }
            }
        }   
    }
}