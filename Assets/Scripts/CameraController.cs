using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public float lerpT;
    public float distance;
    public float angle;
    public Material postFXMaterial;
    public Shader[] drunkShaders;

    private Transform _trackedObject;
    private BloodAlcohol _bac;

    void Start() {
        var player = GameObject.FindGameObjectWithTag(GameTags.Player);

        _trackedObject = player.transform;
        _bac = player.GetComponent<BloodAlcohol>();
    }

    void OnRenderImage (RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, postFXMaterial);
    }

    void Update() {
        postFXMaterial.shader = drunkShaders[(int)(Mathf.Floor(_bac.Current * 10))];
    }

    void LateUpdate() {
        if (_trackedObject == null)
            return;

        var rotation = Quaternion.Euler(angle, 0, 0);
        var targetPosition = _trackedObject.transform.position + rotation * new Vector3(0, 0, -distance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpT);
        transform.LookAt(_trackedObject);
    }
}
