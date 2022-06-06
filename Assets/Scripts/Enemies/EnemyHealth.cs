using System;
using Core;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField] private GameObject xpPrefab;
        
        public static event Action OnDied;

        protected override void Die()
        {
            OnDied?.Invoke();
            Instantiate(xpPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
