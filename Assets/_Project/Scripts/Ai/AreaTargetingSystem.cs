using System;
using _Project.Scripts.Targetable;
using UnityEngine;

namespace _Project.Scripts.Ai
{
    public class AreaTargetingSystem   : MonoBehaviour
    {
        [Header("Scan")]
        [SerializeField,Min(0.1f)] private float _radius = 12f;
        [SerializeField,Min(0.1f)] private float _rescanInterval = 0.1f;
        [SerializeField] private  LayerMask _targetMask;
        [SerializeField] private int _maxTargetsBuffer = 64;

        public Targetable2D Current { get; private set; }

        public event Action<Targetable2D> OnTargetChanged;

        private Collider2D[] _hits;
        private float _timer;
                                      
        private Transform _transform;
        
        public void Awake()
        {
            _transform = transform;
            _hits = new Collider2D[Mathf.Max(1, _maxTargetsBuffer)];
        }
        
        public void SetRange(float range)
        {
            _radius  = range;
        }
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _rescanInterval)
            {
                _timer = 0f;
                Reacquire();
            }
        }

        public void Reacquire()
        {
            if (_hits == null || _hits.Length != _maxTargetsBuffer)
                _hits = new Collider2D[_maxTargetsBuffer];


            int count = Physics2D.OverlapCircleNonAlloc( _transform.position, _radius, _hits, _targetMask);

            Targetable2D best = null;
            float bestScore = float.NegativeInfinity;


            Vector2 origin =_transform.position;
       
            for (int i = 0; i < count; i++)
            {
                var col = _hits[i];
                _hits[i] = null; 
                if (!col) continue;

                if (!col.TryGetComponent<Targetable2D>(out var t))
                    continue;
                if (!t.IsAlive) continue;

                Vector2 to = (Vector2)t.AimPoint - origin;
                float sqr = to.sqrMagnitude;
                float dist = Mathf.Sqrt(sqr);


                TargetContext ctx = new TargetContext
                {
                    target = t,
                    origin = _transform.position,
                    distance = dist,
                    sqrDistance = sqr,
                };


                float score = DefaultScore(in ctx);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = t;
                }
            }

            if (best != Current)
            {
                Current = best;
                OnTargetChanged?.Invoke(Current);
            }
        }

        private float DefaultScore(in TargetContext ctx)
        {
            return -ctx.distance; 
        }
    }
}