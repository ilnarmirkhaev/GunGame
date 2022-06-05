using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        private Transform target;
        private Rigidbody2D rb;
        private Vector2 lookDirection;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.Find("Player").transform;
        }

        private void Update()
        {
            if (!target) return;
            lookDirection = ((Vector2)target.position - rb.position).normalized;
        }

        private void FixedUpdate()
        {
            if (!target) return;
            MoveAndRotate(lookDirection);
        }

        private void MoveAndRotate(Vector2 direction)
        {
            rb.MovePosition(rb.position + direction * (moveSpeed * Time.deltaTime));
        
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}