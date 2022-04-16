using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        // Ignore collisions with the player
        var player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 3);
    }
}
