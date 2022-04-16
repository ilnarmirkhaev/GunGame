using UnityEngine;

public class EnemyStupid : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 lookDirection;
    private float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!target) return;
        lookDirection = ((Vector2)target.position - rb.position).normalized;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        MoveAndRotateCharacter(lookDirection);
    }

    private void MoveAndRotateCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * (moveSpeed * Time.deltaTime));
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Bullet>() != null)
            TakeHit();
    }

    private void TakeHit()
    {
        // TODO: нормальная система урона...
        currentHealth -= 40f;
        
        if (currentHealth <= 0)
            Die();
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
