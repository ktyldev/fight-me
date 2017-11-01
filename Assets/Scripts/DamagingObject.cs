using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour {

    private Health _ownHealth;
    private Health _playerHealth;

	// Use this for initialization
	void Start () {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        _ownHealth = GetComponent<Health>();
        _ownHealth.OnDamageTaken.AddListener(() => {
            if (Random.value > 0.5) {
                _playerHealth.TakeDamage(1);
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
