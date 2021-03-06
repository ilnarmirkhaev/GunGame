using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        private const float CameraZCoordinate = -10f;

        private void Update()
        {
            if (target != null) SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            var targetPosition = target.position;
            transform.position = new Vector3(targetPosition.x, targetPosition.y, CameraZCoordinate);
        }
    }
}
