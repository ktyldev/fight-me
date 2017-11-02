using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GameObject image;
    public GameObject healthObject;

    private Image _image;
    private Health _health;
    
    void Start() {
        _image = image.GetComponent<Image>();

        if (healthObject != null) {
            _health = healthObject.GetComponent<Health>();
        } else {
            _health = GetComponentInParent<Health>();
        }

        if (_health == null)
            throw new System.Exception();
        
        _health.OnChange.AddListener(() => _image.fillAmount = _health.Current);
    }
}
