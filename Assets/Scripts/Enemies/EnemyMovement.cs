using Player;
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
            if (Camera.main != null) target = Camera.main.GetComponent<CameraController>().target;
        }

        private void Update()
        {
            if (!target) return;
            lookDirection = ((Vector2)target.position - rb.position).normalized;

            var dot = Vector2.Dot(Vector2.left, lookDirection);

            GetComponent<SpriteRenderer>().flipX = dot > 0;
        }

        private void FixedUpdate()
        {
            if (!target) return;
            MoveAndRotate(lookDirection);
        }

        private void MoveAndRotate(Vector2 direction)
        {
            rb.MovePosition(rb.position + direction * (moveSpeed * Time.deltaTime));
        }
    }
}