using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacBar : MonoBehaviour {

    public GameObject bacObject;

    private Image _image;
    private BloodAlcohol _bac;

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();
        _bac = bacObject.GetComponent<BloodAlcohol>();

        _bac.OnDrink.AddListener(() => _image.fillAmount = _bac.Current);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
