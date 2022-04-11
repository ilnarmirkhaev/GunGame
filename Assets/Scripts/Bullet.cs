using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
