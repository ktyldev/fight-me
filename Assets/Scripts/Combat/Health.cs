using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public int max;
    private int _current;

    public float Current { get { return (float)_current / max; } }
    public UnityEvent OnChange { get; private set; }
    public UnityEvent OnDeath { get; private set; }

    void Awake() {
        OnChange = new UnityEvent();
        OnDeath = new UnityEvent();
    }

    // Use this for initialization
    void Start() {
        _current = max;
    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeDamage(int amount) {
        _current -= amount;
        OnChange.Invoke();
        if (Current <= 0) {
            OnDeath.Invoke();
            Destroy(gameObject);
        }
    }

    public void Restore(int amount) {
        _current = Mathf.Clamp(_current + amount, _current, max);
        OnChange.Invoke();
    }
}
