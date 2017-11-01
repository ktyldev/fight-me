using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform trackedObject;
    public float lerpT;
    public float distance;
    public float angle;

    void LateUpdate() {
        if (trackedObject == null)
            return;

        var rotation = Quaternion.Euler(angle, 0, 0);
        var targetPosition = trackedObject.transform.position + rotation * new Vector3(0, 0, -distance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpT);
        transform.LookAt(trackedObject);
    }
}
