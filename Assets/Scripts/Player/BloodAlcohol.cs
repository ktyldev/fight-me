using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodAlcohol : MonoBehaviour {

    public int maximum = 100;
    public float drinkTime;

    private int _current;
    public float Current { get { return (float)_current / maximum; } }
    public UnityEvent OnDrink { get; private set; }
    public bool IsDrinking { get; private set; }

    private SfxManager _sfx;

    void Awake() {
        OnDrink = new UnityEvent();    
    }

    // Use this for initialization
    void Start () {
        _sfx = GameObject.FindGameObjectWithTag(GameTags.Music).GetComponent<SfxManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Drink(Drink drink) {
        if (_current == maximum) {
            GameObject.FindGameObjectWithTag(GameTags.Spawner).GetComponent<Spawner>().SpawnBoss();
            return;
        }
        
        _current = Mathf.Clamp(_current + drink.bacIncrease, 0, maximum);
        StartCoroutine(DrinkDelay());
        OnDrink.Invoke();
        _sfx.Drink();
    }

    private IEnumerator DrinkDelay() {
        var animator = GetComponentInChildren<Animator>();

        IsDrinking = true;
        animator.SetTrigger(GameTags.anim_drink);
        animator.SetBool(GameTags.anim_moving, false);

        yield return new WaitForSeconds(drinkTime);

        IsDrinking = false;
    }
}
