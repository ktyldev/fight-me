using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject boss;

    private bool _bossSpawned;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SpawnBoss() {
        if (_bossSpawned)
            return;

        _bossSpawned = true;
        Instantiate(boss, transform);
    }
}
