using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SpawnManager _SpawnManager;
    void Start()
    {
        _SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_SpawnManager == null) Debug.LogError("Spawn manager is null");
        _SpawnManager.StartSpawning();
        Destroy(this.gameObject, 2.8f);
    }


}
