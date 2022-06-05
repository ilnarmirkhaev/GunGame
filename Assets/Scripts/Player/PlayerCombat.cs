using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Transform sword;
        [SerializeField] private float attackRate = 2f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float knockBack = 3f;
        
        
        private float _attackFrequency;
        private float _nextAttackTime;

        private void Awake()
        {
            _attackFrequency = 1f / attackRate;
        }

        private void Update()
        {
            if (!(Time.time >= _nextAttackTime)) return;
            if (!Input.GetButtonDown("Fire1")) return;
            
            Attack();
            _nextAttackTime = Time.time + _attackFrequency;
        }

        private void Attack()
        {
            sword.RotateAround(transform.position, Vector3.forward, 60f);
            
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