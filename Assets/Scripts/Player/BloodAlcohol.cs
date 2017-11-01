using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodAlcohol : MonoBehaviour {

    public int maximum = 100;

    private int _current;
    public float Current { get { return (float)_current / maximum; } }
    public UnityEvent OnDrink { get; private set; }

    void Awake() {
        OnDrink = new UnityEvent();    
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Increase(int amount) {
        if (_current == maximum)
            return;
        
        _current = Mathf.Clamp(_current + amount, 0, maximum);
        OnDrink.Invoke();
    }
}
