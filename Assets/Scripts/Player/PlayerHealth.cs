using System;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : CharacterHealth
    {
        public static event Action OnHurt;
        public static event Action OnDied;
        
        public override void TakeHit(float damage)
        {
            base.TakeHit(damage);
            OnHurt?.Invoke();
        }

        protected override void Die()
        {
            OnDied?.Invoke();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}