using System;
using UnityEngine;

namespace Core
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float acceleration;
    
        private Transform _player;
        private Transform _transform;
    
        public static event Action OnXpGained;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_player == null) return;
        
            var position = _transform.position;
            position += (_player.position - position).normalized * Time.deltaTime * speed;
            _transform.position = position;
            speed += acceleration * Time.deltaTime;
        
            if ((position - _player.position).magnitude < 1f)
                GainXp();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                _player = col.transform;
            }
        }

        private void GainXp()
        {
            OnXpGained?.Invoke();
            Destroy(gameObject);
        }
    }
}