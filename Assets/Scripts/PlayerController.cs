using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed, jumpStrength;
    public LayerMask groundLayer;

    private Rigidbody _rigidBody;
    private bool _jump;

    // Use this for initialization
    void Start() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (!_jump) {
            _jump = Input.GetButtonDown("Jump");
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rigidBody.AddForce(movement * speed);

        if (_jump) {
            if (IsGrounded()) {
                _rigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            }

            _jump = false;
        }
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, groundLayer.value);
    }
}
