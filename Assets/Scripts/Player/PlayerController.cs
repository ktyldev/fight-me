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
            var input = new Vector3(Input.GetAxis(GameInput.Horizontal), 0, Input.GetAxis(GameInput.Vertical));
            var moveDir = Vector3.Lerp(input, _movement.normalized, _bac.Current * drunkMotion);
            _movement = moveDir * speed;

            if (_movement != Vector3.zero) {
                // Look in the direction of movement, or direction last moved in if standing still
                transform.LookAt(transform.position + (_movement.magnitude > 1 ? _movement : transform.forward));
            }
            
            if (Input.GetButtonDown(GameInput.Jump)) {
                _movement.y = jumpStrength;
            }
        }

        _movement.y += Physics.gravity.y * Time.deltaTime;
        _charController.Move(_movement * speed * Time.deltaTime);

        if (Input.GetButtonDown(GameInput.Fire)) {
            Punch();
        }
    }

    private void Punch() {
        GetComponent<MouseLook>().LookAtMouse();

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
