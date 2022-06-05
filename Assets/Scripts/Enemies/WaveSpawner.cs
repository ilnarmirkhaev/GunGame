using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WaveSpawner : MonoBehaviour
    {
        private enum SpawnState
        {
            Spawning,
            Waiting,
            Counting
        }
    
        [System.Serializable]
        public class Wave
        {
            public string name;
            public GameObject enemy;
            public int count;
            public float spawnRate;
        }

        public Wave[] waves;
        public Transform[] spawnPoints;
        private int waveIndex = 0;

        public float timeBetweenWaves = 3f;
        private float waveCountdown;
        private SpawnState state = SpawnState.Counting;

        private int _aliveEnemies;

        private void Start()
        {
            if (spawnPoints.Length == 0)
                Debug.LogError("No spawn points referenced");
        
            waveCountdown = timeBetweenWaves;
        }

        private void OnEnable()
        {
            EnemyHealth.OnDied += EnemyDied;
        }

        private void OnDisable()
        {
            EnemyHealth.OnDied -= EnemyDied;
        }

        private void EnemyDied()
        {
            _aliveEnemies--;
        }

        private void Update()
        {
            if (state == SpawnState.Waiting)
            {
                if (!EnemyIsAlive())
                    WaveCompleted();
                return;
            }
        
            if (waveCountdown <= 0)
            {
                if (state != SpawnState.Spawning)
                {
                    StartCoroutine(SpawnWaveCoroutine(waves[waveIndex]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }

        private void WaveCompleted()
        {
            Debug.Log("Wave Completed!");
        
            state = SpawnState.Counting;
            waveCountdown = timeBetweenWaves;

            if (waveIndex >= waves.Length - 1)
            {
                waveIndex = 0;
                Debug.Log("ALL WAVES COMPLETE! Looping...");
            }
            else
            {
                waveIndex++;
            }
        }

        private bool EnemyIsAlive() => _aliveEnemies > 0;

        private IEnumerator SpawnWaveCoroutine(Wave wave)
        {
            Debug.Log("Spawning Wave: " + wave.name);
            state = SpawnState.Spawning;
            for (var i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1 / wave.spawnRate);
            }
            state = SpawnState.Waiting;
        }

        private void SpawnEnemy(GameObject enemy)
        {
            var sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
            _aliveEnemies++;
        }
    }
}
