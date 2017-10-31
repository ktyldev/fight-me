using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int max;

    public int current { get; private set; }

	// Use this for initialization
	void Start () {
        current = max;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount) {
        current -= amount;
        if (current <= 0) {
            Destroy(gameObject);
        }
    }
}
