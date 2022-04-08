using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        var movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        MovePlayer(movementVector);
    }

    private void MovePlayer(Vector3 movementVector)
    {
        transform.position += movementVector * (moveSpeed * Time.deltaTime);
    }
}
