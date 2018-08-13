using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{   
    public enum SpawnState { SPAWING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 2f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;


    private void Start()
    {

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("no spawn points found");
        }

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        // Handle each state and what should happen
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWING)
            {
                StartCoroutine(SpawnWave ( waves[nextWave] )); 
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //End the game
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }
        else
        {
            nextWave++;
        }
       
    }


    bool EnemyIsAlive()
    {
        // Check if there is at least 1 enemy alive

        return GameObject.FindObjectOfType<EnemyController>();
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        // Spawn the next wave
        state = SpawnState.SPAWING;

        for( int i = 0; i < _wave.count; i ++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds( 1f/ _wave.rate );
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        // Spawn a single enemy
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }


}