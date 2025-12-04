using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Weapon.ProjectileBase
{
    public class ProjectileVisual : MonoBehaviour
    {
        [SerializeField] private Transform _projectileVisual;
        [SerializeField] private Transform _projectileShadow;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField]  private float _shadowPositionDivider = 6f;

        private bool _toTarget = false;
        
        private Vector3 _targetPosition;
        private Vector3 _trajectoryStartPosition;

        private Transform _target;
        
        private void OnEnable()
        {
            _projectile.OnActive += Active;
            _trailRenderer.Clear();

        }
        private void OnDisable()
        {
            _projectile.OnActive -= Active;
            _trailRenderer.Clear();
        }
        private void EnableTrailAndShadow()
        {
            _projectileShadow.gameObject.SetActive(true);
            _trailRenderer.gameObject.SetActive(true);
        }
        private void EnableVisual()
        {
            _projectileVisual.gameObject.SetActive(true);
        }
        private void Active()
        {
            EnableTrailAndShadow();
            EnableVisual();
            _trajectoryStartPosition = transform.position;
            _toTarget = false;
        }
        private void UpdateShadowPosition()
        {
            Vector3 newPosition = transform.position;
            Vector3 trajectoryRange;
            if (_target != null)
            {
                trajectoryRange = _target.position - _trajectoryStartPosition;
            }
            else
            {
                trajectoryRange = _targetPosition - _trajectoryStartPosition;
            }

            if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
            {
                // Projectile is curved on the X axis
                newPosition.x = _trajectoryStartPosition.x +
                                _projectile.GetNextXTrajectoryPosition() / _shadowPositionDivider +
                                _projectile.GetNextPositionXCorrectionAbsolute();

            }
            else
            {
                // Projectile is curved on the Y axis
                newPosition.y = _trajectoryStartPosition.y  +
                                _projectile.GetNextYTrajectoryPosition() / _shadowPositionDivider +
                                _projectile.GetNextPositionYCorrectionAbsolute();
            }


            _projectileShadow.position = newPosition;
        }
        private void UpdateProjectileRotation()
        {
            Vector3 projectileMoveDir = _projectile.GetProjectileMoveDir();
            _projectileVisual.transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
        }
        private void UpdateProjectileShadowRotation()
        {
            Vector3 projectileMoveDir = _projectile.GetProjectileMoveDir();
            _projectileShadow.transform.rotation = Quaternion.Euler(0, 0,
                Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
        }
        public void UpdateVisual()
        {
            UpdateProjectileRotation();
            UpdateShadowPosition();

            float trajectoryProgressMagnitude = (transform.position - _trajectoryStartPosition).magnitude;
            float trajectoryMagnitude;
            
            if (_target!= null)
            {
                trajectoryMagnitude = (_target.position - _trajectoryStartPosition).magnitude;
            }
            else
            {
                trajectoryMagnitude = (_targetPosition - _trajectoryStartPosition).magnitude;
            }


            float trajectoryProgressNormalized = trajectoryProgressMagnitude / trajectoryMagnitude;

            if (trajectoryProgressNormalized < .7f)
            {
                UpdateProjectileShadowRotation();
            }

        }
       
        public void SetTarget(Transform target)
        {
            _target = target;
            _toTarget = true;
        }
        public void SetTarget(Vector2 target)
        {
            _targetPosition = target;
        }
    }
}