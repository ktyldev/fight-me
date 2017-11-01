using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour {

    public int bacIncrease;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null)
            return;

        var playerBac = player.GetComponent<BloodAlcohol>();
        playerBac.Increase(bacIncrease);
        Destroy(gameObject);
    }
}
