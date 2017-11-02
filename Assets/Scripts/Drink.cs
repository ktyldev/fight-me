using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour {

    public int healthIncrease;
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
        
        player.GetComponent<Health>().Restore(healthIncrease);
        player.GetComponent<BloodAlcohol>().Increase(bacIncrease);
        Destroy(gameObject);
    }
}
