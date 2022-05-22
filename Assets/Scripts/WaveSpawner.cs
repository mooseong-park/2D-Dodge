using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum EnemyState
    {
        RANDOM,
        ROW_TOP,
        ROW_BOTTOM,
        ROW_TandB,
        COLUMN_LEFT,
        COLUMN_RIGHT,
        COLUMN_RandL,
        RECTANGLE,
    }

    
    [System.Serializable]
    public class Wave
    {
        public string name;
        //public Transform enemy;
        public EnemyState EState;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public int nextWave;

    public Transform[] spawnPoints;

    public float waveCountdown = 5f;


    // Start is called before the first frame update
    void Start()
    {
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(waveCountdown <= 0)
        {
            nextWave = Random.Range(0, waves.Length);
            StartCoroutine(SpawnWave(waves[nextWave]));
            waveCountdown = Random.Range(3, 6);
        }

        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.EState);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        yield break;
    }

    void SpawnEnemy(EnemyState enemyState)
    {
        //Debug.Log("Spawning Enemy: " + _enemy.name);

        switch(enemyState)
        {
            case EnemyState.RANDOM:
                {
                    Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    ObjectPooler.SpawnFromPool<Enemy_G>("Enemy_G", _sp.position, _sp.rotation);
                    break;
                }
            case EnemyState.ROW_TOP:
                {
                    for (int i = 0; i < 17; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", _sp.position, _sp.rotation);
                    }
                    ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", spawnPoints[34].position, spawnPoints[34].rotation);
                    ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", spawnPoints[35].position, spawnPoints[35].rotation);
                    break;
                }
            case EnemyState.ROW_BOTTOM:
                {
                    for (int i = 17; i < 34; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", _sp.position, _sp.rotation);
                    }
                    ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", spawnPoints[36].position, spawnPoints[36].rotation);
                    ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", spawnPoints[37].position, spawnPoints[37].rotation);
                    break;
                }
            case EnemyState.ROW_TandB:
                {
                    for (int i = 0; i < 38; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_V>("Enemy_V", _sp.position, _sp.rotation);
                    }
                    break;
                }

            case EnemyState.COLUMN_LEFT:
                {
                    for (int i = 38; i < 43; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", _sp.position, _sp.rotation);
                    }
                    ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", spawnPoints[34].position, spawnPoints[34].rotation);
                    ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", spawnPoints[36].position, spawnPoints[36].rotation);
                    break;
                }
            case EnemyState.COLUMN_RIGHT:
                {
                    for (int i = 43; i < 48; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", _sp.position, _sp.rotation);
                    }
                    ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", spawnPoints[35].position, spawnPoints[35].rotation);
                    ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", spawnPoints[37].position, spawnPoints[37].rotation);
                    break;
                }
            case EnemyState.COLUMN_RandL:
                {
                    for (int i = 34; i < 48; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_H>("Enemy_H", _sp.position, _sp.rotation);
                    }
                    break;
                }

            case EnemyState.RECTANGLE:
                {
                    for (int i = 0; i < 48; i++)
                    {
                        Transform _sp = spawnPoints[i];
                        ObjectPooler.SpawnFromPool<Enemy_G>("Enemy_G", _sp.position, _sp.rotation);
                    }
                    break;
                }
        }
    }
}
