using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class DeathZone : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out EnemyFacade enemy))
            {
                enemy.DeathEvent();                                                        
            }

        }
    }
}