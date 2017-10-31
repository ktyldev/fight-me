using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform focus;
    public float lerpT;
    public float distance;
    public float angle;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        var rotation = Quaternion.Euler(angle, 0, 0);
        var targetPosition = focus.position + rotation * new Vector3(0, 0, -distance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpT);
        transform.LookAt(focus);
    }
}
