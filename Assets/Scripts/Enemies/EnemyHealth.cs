using System;

namespace Enemies
{
    public class EnemyHealth : CharacterHealth
    {
        public static event Action OnDied;

        protected override void Die()
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
