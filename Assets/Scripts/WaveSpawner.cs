using System.Collections;
using UnityEngine;

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
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points referenced");
        
        waveCountdown = timeBetweenWaves;
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

    // TODO: need optimization
    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (!(searchCountdown <= 0f)) return true;
        searchCountdown = 1f;
        return GameObject.FindWithTag("Enemy") != null;

    }

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
    }
}
