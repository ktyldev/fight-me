using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    private Plane _plane;

	// Use this for initialization
	void Start () {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        if (!_plane.Raycast(ray, out distance))
            return;

        var dir = ray.GetPoint(distance) - transform.position;
        transform.LookAt(transform.position + new Vector3(dir.x, 0, dir.z).normalized);
    }
}
