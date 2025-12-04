using _Project.Scripts.Targetable;
using UnityEngine;

namespace _Project.Scripts.Ai
{
    public struct TargetContext
    {
        public Targetable2D target;
        public Vector3 origin;
        public float distance;
        public float sqrDistance;
    }
}