using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private float damage;
        private PlayerHealth player;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<PlayerHealth>() != null)
            {
                player = col.gameObject.GetComponent<PlayerHealth>();
            }
        }

        private void OnCollisionStay2D()
        {
            if (player != null)
            {
                player.TakeHit(damage);
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<PlayerHealth>() != null)
            {
                player = null;
            }
        }
    }
}