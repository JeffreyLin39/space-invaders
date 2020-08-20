using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleshotPrefab;
    [SerializeField]
    private GameObject _SpeedboostPrefab;
    [SerializeField]
    private GameObject _ShieldPrefab;
    private bool _stopSpawning = false;
    private int random = 0;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void playerDeath()
    {
        _stopSpawning = true;
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning)
        {
            random = Random.Range(1, 101);
            if (random > 0 && random < 6) Instantiate(_tripleshotPrefab, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            if (random > 5 && random < 11) Instantiate(_SpeedboostPrefab, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            if (random > 10 && random < 16) Instantiate(_ShieldPrefab, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
