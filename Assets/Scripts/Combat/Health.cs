using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public int max;
    private int _current;

    public float Current { get { return (float)_current / max; } }
    public UnityEvent OnDamageTaken { get; private set; }

    void Awake() {
        OnDamageTaken = new UnityEvent();    
    }

    // Use this for initialization
    void Start () {
        _current = max;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount) {
        _current -= amount;
        OnDamageTaken.Invoke();
        if (Current <= 0) {
            Destroy(gameObject);
        }
    }
}
