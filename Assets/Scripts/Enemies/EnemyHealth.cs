using System;
using Core;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField] private GameObject xpPrefab;
        
        public static event Action<GameObject> OnDied;

        protected override void Die()
        {
            Instantiate(xpPrefab, transform.position, Quaternion.identity);
            OnDied?.Invoke(gameObject);
            gameObject.SetActive(false);
        }
    }
}
