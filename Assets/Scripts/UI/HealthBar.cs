using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    public GameObject image;
    public GameObject healthObject;
    public bool showAtStart;

    private Image _image;
    private Health _health;
    
    void Start() {
        _image = image.GetComponent<Image>();

        if (!showAtStart) {
            _image.transform.parent.gameObject.SetActive(false);
        }

        if (healthObject != null) {
            _health = healthObject.GetComponent<Health>();
        } else {
            _health = GetComponentInParent<Health>();
        }

        if (_health == null)
            throw new System.Exception();
        
        _health.OnChange.AddListener(() => {
            if (!_image.IsActive()) {
                _image.transform.parent.gameObject.SetActive(true);
            }
            _image.fillAmount = _health.Current;
        });
    }
}
