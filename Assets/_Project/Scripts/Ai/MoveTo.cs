using System;
using UnityEngine;

namespace _Project.Scripts.Ai
{
    public class MoveTo 
    {
        private float _speed = 5;
        private Rigidbody2D _rigidbody;

        public MoveTo(Rigidbody2D rigidbody,float speed = 5)
        {
            _speed = speed;
            _rigidbody = rigidbody;
        }

        public void FixedTick()
        {
            _rigidbody.linearVelocity = Vector2.down * _speed;        
        }
    }
}