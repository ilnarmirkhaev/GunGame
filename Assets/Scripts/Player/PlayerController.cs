using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Camera cam;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lookDirection;
    private Vector2 mousePosition;

    private float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        MoveAndRotatePlayer(movement, lookDirection);
    }

    private void HandleInput()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = mousePosition - rb.position;
    }

    private void MoveAndRotatePlayer(Vector2 movementVector, Vector2 rotationVector)
    {
        rb.MovePosition(rb.position + movementVector * moveSpeed * Time.deltaTime);

        var angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            TakeHit();
    }

    private void TakeHit()
    {
        // TODO: нормальная система урона...
        currentHealth -= 20f;
        Debug.Log("Damage taken! Health: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("You died!");
        Destroy(gameObject);
    }
}