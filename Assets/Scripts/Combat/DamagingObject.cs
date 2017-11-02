using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour {

    [SerializeField]
    private int _returnDamage = 1;

    [SerializeField]
    [Range(0.05f, 1f)]
    private float _damageChance;

    private Health _ownHealth;
    private Health _playerHealth;

	// Use this for initialization
	void Start () {
        _playerHealth = GameObject.FindGameObjectWithTag(GameTags.Player).GetComponent<Health>();
        _ownHealth = GetComponent<Health>();
        _ownHealth.OnChange.AddListener(() => {
            if (Random.Range(0f, 1f) <= _damageChance) {
                _playerHealth.TakeDamage(_returnDamage);
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
