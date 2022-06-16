using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        
        [SerializeField] private Transform spawnPointsPool;
        [SerializeField] private Transform enemyPool;

        [SerializeField] private float spawnRate = 2f;
        private int _spawnTime;
        
        [SerializeField] private int enemyCapacity = 30;
        private Queue<GameObject> _enemiesToSpawn;

        private List<Transform> _spawnPoints;

        private bool _isSpawning;

        private void Awake()
        {
            _spawnTime = (int)(1000 / spawnRate);
            
            _spawnPoints = new List<Transform>();
            foreach (Transform spawnPoint in spawnPointsPool)
            {
                _spawnPoints.Add(spawnPoint);
            }

            _enemiesToSpawn = new Queue<GameObject>();
            for (var i = 0; i < enemyCapacity; i++)
            {
                var enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, enemyPool);
                enemy.SetActive(false);
                _enemiesToSpawn.Enqueue(enemy);
            }
        }

        private void Start()
        {
            if (_spawnPoints.Count == 0)
                Debug.LogError("No spawn points found");
        }

        private void OnEnable()
        {
            EnemyHealth.OnDied += EnemyDied;
        }

        private void OnDisable()
        {
            EnemyHealth.OnDied -= EnemyDied;
        }

        private async void Update()
        {
            if (_enemiesToSpawn.Count != 0)
                SpawnEnemy();
            
            await Task.Delay(_spawnTime);
        }

        private void EnemyDied(GameObject enemy)
        {
            _enemiesToSpawn.Enqueue(enemy);
        }

        private void SpawnEnemy()
        {
            var sp = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            
            var enemy = _enemiesToSpawn.Dequeue();
            enemy.transform.position = sp.position;
            enemy.transform.rotation = sp.rotation;
            enemy.SetActive(true);
        }
    }
}
