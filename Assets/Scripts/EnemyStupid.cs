using UnityEngine;

public class EnemyStupid : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private GameObject player;
    private Rigidbody2D playerRB;

    private Rigidbody2D rb;
    private Vector2 lookDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lookDirection = (playerRB.position - rb.position).normalized;
    }

    private void FixedUpdate()
    {
        MoveAndRotateCharacter(lookDirection);
    }

    private void MoveAndRotateCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Bullet>() != null)
            Destroy(gameObject);
    }
}
