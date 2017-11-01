using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PickupDropper : MonoBehaviour {

    public GameObject pickup;
    
	// Use this for initialization
	void Start () {
        GetComponent<Health>()
            .OnDeath
            .AddListener(() => Instantiate(pickup, transform.position, Quaternion.identity));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
