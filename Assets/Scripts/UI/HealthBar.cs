using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GameObject healthObject;

    private Image _image;
    private Health _health;

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();
        _health = healthObject.GetComponent<Health>();

        _health.OnDamageTaken.AddListener(() => _image.fillAmount = _health.Current);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
