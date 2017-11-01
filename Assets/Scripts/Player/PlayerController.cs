using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpStrength;
    public float punchReach;
    public float drunkMotion;
    public LayerMask groundLayer;

    private CharacterController _charController;
    private Health _health;
    private BloodAlcohol _bac;
    
    private Vector3 _movement = Vector3.zero;

    void Start() {
        _health = GetComponent<Health>();
        _bac = GetComponent<BloodAlcohol>();
        _health.OnDeath.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));

        _charController = GetComponent<CharacterController>();
    }

    private void Update() {
        if (_charController.isGrounded) {
            var moveDir = Vector3.Lerp(
                new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 
                _movement.normalized, 
                _bac.Current * drunkMotion);
            _movement = moveDir * speed;

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
