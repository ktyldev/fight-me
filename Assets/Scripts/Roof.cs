using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : MonoBehaviour {

    private GameObject _player;
    private MeshRenderer _roof;
    
    void Start() {
        _player = GameObject.FindGameObjectWithTag(GameTags.Player);
        _roof = transform.GetComponentInChildren<MeshRenderer>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject != _player)
            return;
        
        _roof.gameObject.SetActive(false);
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject != _player)
            return;

        _roof.gameObject.SetActive(true);
    }
}
