using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

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

        Destroy(gameObject);
    }
}
