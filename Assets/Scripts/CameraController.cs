using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool isFollowing;
    [SerializeField] private Transform target;
    private const float CameraZCoordinate = -10f;

    private void Update()
    {
        if (!isFollowing) return;
        if (target != null) SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        var targetPosition = target.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, CameraZCoordinate);
    }
}
