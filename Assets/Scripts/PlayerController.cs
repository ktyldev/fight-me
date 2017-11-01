using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpStrength;
    public float punchReach;
    public LayerMask groundLayer;

    private CharacterController _charController;
    private Vector3 _movement = Vector3.zero;

    void Start() {
        _charController = GetComponent<CharacterController>();
    }

    private void Update() {
        if (_charController.isGrounded) {
            _movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _movement *= speed;

            if (Input.GetButtonDown("Jump")) {
                _movement.y = jumpStrength;
            }
        }

        _movement.y += Physics.gravity.y * Time.deltaTime;
        _charController.Move(_movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire")) {
            Punch();
        }
    }

    private void Punch() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, punchReach)) {
            var hitObject = hit.collider.gameObject;
            var opponentHealth = hitObject.GetComponent<Health>();
            if (opponentHealth == null)
                return;

            opponentHealth.TakeDamage(1);
        }
    }
}
