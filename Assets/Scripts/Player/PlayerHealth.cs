using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : CharacterHealth
    {
        public static event Action OnHurt;
        public static event Action OnDied;
        private bool _onCooldown;

        public override void TakeHit(float damage)
        {
            if (_onCooldown) return;
            
            base.TakeHit(damage);
            OnHurt?.Invoke();
            StartCoroutine(CooldownCoroutine(0.5f));
        }

        protected override void Die()
        {
            OnDied?.Invoke();
            GetComponent<Collider2D>().enabled = false;
        }

        private IEnumerator CooldownCoroutine(float seconds)
        {
            _onCooldown = true;
            yield return new WaitForSeconds(seconds);
            _onCooldown = false;
        }
    }
}