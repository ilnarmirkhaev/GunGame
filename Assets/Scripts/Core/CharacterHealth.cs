using UnityEngine;

namespace Core
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float _health;

        private float Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health <= 0)
                    Die();
            }
        }

        private void Awake()
        {
            Health = maxHealth;
        }

        public void TakeHit(float damage)
        {
            Health -= damage;
        }
    
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}