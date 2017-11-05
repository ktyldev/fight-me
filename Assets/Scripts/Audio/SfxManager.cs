using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour {

    public GameObject breaking;
    public GameObject drink;
    public GameObject punch;
    public GameObject[] steps;
    
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    
    public void Step() {
        Play(steps[Random.Range(0, steps.Length)]);
    }

    public void Punch() {
        Play(punch);
    }

    public void Breaking() {
        Play(breaking);
    }

    public void Drink() {
        Play(drink);
    }

    private void Play(GameObject sound, float destroyAfter = 1) {
        Destroy(Instantiate(sound, transform), destroyAfter);
    }
}
