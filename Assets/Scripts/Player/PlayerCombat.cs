using System;
using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float knockBack = 3f;

        private void OnEnable()
        {
            HeroKnight.OnAttacked += Attack;
        }

        private void OnDisable()
        {
            HeroKnight.OnAttacked -= Attack;
        }
        
        private void Attack()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeHit(damage);
                KnockBack(enemy.attachedRigidbody);
            }
        }

        private void KnockBack(Rigidbody2D enemy)
        {
            var diff = (Vector2)(enemy.transform.position - transform.position).normalized;
            enemy.AddForce(diff * knockBack);
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint != null)
                Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}