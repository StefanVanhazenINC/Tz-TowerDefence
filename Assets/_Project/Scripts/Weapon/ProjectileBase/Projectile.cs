using System;
using _Project.Scripts.Targetable;
using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Project.Scripts.Weapon.ProjectileBase
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        #region PoolSetting

        public event Action<IProjectile> DisableCallback;

        public void SetProjectile(BulletInfo info)
        {
        }
      
        public void Active()
        {
            _isStop = false;
            gameObject.SetActive(true);
            OnActive?.Invoke();
        }

        #endregion

        #region Projectile

        [FormerlySerializedAs("projectileVisual")] [SerializeField] private ProjectileVisual _projectileVisual;
        
        [SerializeField] private bool disableAfterFinishWay = true;
        
        [SerializeField] private bool disableAfterTime = false;
        [SerializeField, ShowIf("disableAfterTime")] private float disableTimer = 1;

        [SerializeField] public UnityEvent OnEventAfterFinishWay;
        private DamageInfo _damageInfo;
        private bool ToTarget = false;
        private Targetable2D target;
        private Vector3 targetPosition;
        private float moveSpeed;
        private float maxMoveSpeed;
        private float trajectoryMaxRelativeHeight;

        private AnimationCurve trajectoryAnimationCurve;
        private AnimationCurve axisCorrectionAnimationCurve;
        private AnimationCurve projectileSpeedAnimationCurve;

        private Vector3 trajectoryStartPoint;
        private Vector3 projectileMoveDir;
        private Vector3 trajectoryRange;

        private float nextYTrajectoryPosition;
        private float nextXTrajectoryPosition;
        private float nextPositionYCorrectionAbsolute;
        private float nextPositionXCorrectionAbsolute;

        public event Action OnActive;

        private bool _isStop = true;
       

        public Targetable2D Target => target;
        public bool DisableAfterFinishWay => disableAfterFinishWay;
        public DamageInfo DamageInfo => _damageInfo;
        
        public void SetProjectile(DamageInfo info)
        {
            _damageInfo = info;
            
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            
        }

        public void SetDisableAfterFinishWay(bool value)
        {
            disableAfterFinishWay = value;
        }
         
        public void DestroyProjectile()
        {
            if (ToTarget)
            {
                target.TakeDamage(_damageInfo);
            }                         
            OnEventAfterFinishWay?.Invoke();    
        
                
            if (disableAfterFinishWay)
            {
                DisableCallback?.Invoke(this);   
            }
            else if (disableAfterTime)
            {
                DelayDisable().Forget(); 
            }
            
        }
        private async UniTaskVoid DelayDisable()
        {
            await UniTask.WaitForSeconds(disableTimer);
            DisableCallback?.Invoke(this);   
        }

        public void DisableFromOutside()
        {
            DisableCallback?.Invoke(this);    
        }

        public void Disable()
        {
            ToTarget = false;
            gameObject.SetActive(false);
        }

        private float _tempDistance;
        private void Update()
        {
            if (!_isStop)
            {
                UpdateProjectilePosition();
                _projectileVisual.UpdateVisual();  
            }
            
        }

        public void InitializeProjectile(Targetable2D target, float maxMoveSpeed, float trajectoryMaxHeight)
        {
            ToTarget = true;
            
            trajectoryStartPoint = transform.position; 
            this.target = target;
            
            this.maxMoveSpeed = maxMoveSpeed;

            float xDistanceToTarget = target.AimPoint.x - transform.position.x;
            this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;
            _projectileVisual.SetTarget(target.transform);
            Update();
            Active();
        }

        public void InitializeProjectile(Vector2 target, float maxMoveSpeed, float trajectoryMaxHeight)
        {
            trajectoryStartPoint = transform.position; 
            targetPosition = target;
            this.maxMoveSpeed = maxMoveSpeed;
            float xDistanceToTarget = targetPosition.x - transform.position.x;
            this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;
            _projectileVisual.SetTarget(target);

            Update();
            Active();
        }
        private void UpdateProjectilePosition()
        {
            if (ToTarget)
            {
                trajectoryRange = target.AimPoint - trajectoryStartPoint;
            }
            else
            {
                trajectoryRange = targetPosition - trajectoryStartPoint;

            }


            if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
            {
                // Projectile will be curved on the X axis

                if (trajectoryRange.y < 0)
                {
                    // Target is located under shooter
                    moveSpeed = -moveSpeed;
                }

                UpdatePositionWithXCurve();
            }
            else
            {
                // Projectile will be curved on the Y axis

                if (trajectoryRange.x < 0)
                {
                    // Target is located behind shooter
                    moveSpeed = -moveSpeed;
                }

                UpdatePositionWithYCurve();
            }
        }

        private void UpdatePositionWithXCurve()
        {
            float nextPositionY = transform.position.y + moveSpeed * Time.deltaTime;
            float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;
           

            
            float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
            nextXTrajectoryPosition = nextPositionXNormalized * trajectoryMaxRelativeHeight;

            float nextPositionXCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
            nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * trajectoryRange.x;

            if (trajectoryRange.x > 0 && trajectoryRange.y > 0)
            {
                nextXTrajectoryPosition = -nextXTrajectoryPosition;
            }

            if (trajectoryRange.x < 0 && trajectoryRange.y < 0)
            {
                nextXTrajectoryPosition = -nextXTrajectoryPosition;
            }


            float nextPositionX = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;

            Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

            CalculateNextProjectileSpeed(nextPositionYNormalized);
           // projectileMoveDir = newPosition - transform.position;
            if (!_isStop && nextPositionYNormalized >= 1f - 1e-6f)
            {
                _isStop=true;
                StopMove();
                DestroyProjectile();   
            }
           
            if (!_isStop )
            {
                projectileMoveDir = newPosition - transform.position;
                transform.position = newPosition;
            }
            
        }

        private void StopMove()
        {
            if (target)
            {
                transform.position = target.AimPoint;
            }
            else
            {
                transform.position = targetPosition;
            }
        }

        private void UpdatePositionWithYCurve()
        {
            float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
            float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

         


       
            
            float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
            nextYTrajectoryPosition = nextPositionYNormalized * trajectoryMaxRelativeHeight;

            float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
            nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;

            float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;

            Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

            CalculateNextProjectileSpeed(nextPositionXNormalized);
           
           
            if (!_isStop && nextPositionXNormalized >= 1f - 1e-6f)
            {
                _isStop=true;
                StopMove();
                DestroyProjectile();   
            }
            if (!_isStop )
            {
                projectileMoveDir = newPosition - transform.position;
                transform.position = newPosition;
            }
        }

        private void CalculateNextProjectileSpeed(float nextPositionXNormalized)
        {
            float nextMoveSpeedNormalized = projectileSpeedAnimationCurve.Evaluate(nextPositionXNormalized);

            moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed;
        }


        public void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve,
            AnimationCurve axisCorrectionAnimationCurve, AnimationCurve projectileSpeedAnimationCurve)
        {
            this.trajectoryAnimationCurve = trajectoryAnimationCurve;
            this.axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
            this.projectileSpeedAnimationCurve = projectileSpeedAnimationCurve;
        }

        public Vector3 GetProjectileMoveDir()
        {
            return projectileMoveDir;
        }

        public float GetNextYTrajectoryPosition()
        {
            return nextYTrajectoryPosition;
        }

        public float GetNextPositionYCorrectionAbsolute()
        {
            return nextPositionYCorrectionAbsolute;
        }

        public float GetNextXTrajectoryPosition()
        {
            return nextXTrajectoryPosition;
        }

        public float GetNextPositionXCorrectionAbsolute()
        {
            return nextPositionXCorrectionAbsolute;
        }

        #endregion
    }

}