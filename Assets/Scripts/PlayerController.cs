using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed, jumpStrength;
    public LayerMask groundLayer;

    private Rigidbody _rigidBody;

    // Use this for initialization
    void Start() {
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate() {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rigidBody.AddForce(movement * speed);

        if (Physics.Raycast(transform.position, Vector3.down, 1.0f, groundLayer.value) && Input.GetButtonDown("Jump"))
            _rigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }
}
