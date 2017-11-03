using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerCombat))]
public class PlayerController : MonoBehaviour {

    public float speed;
    public float drunkMotion;
    public float fallAnimTime;

    private CharacterController _charController;
    private Health _health;
    private BloodAlcohol _bac;
    private PlayerCombat _combat;
    private Vector3 _movement = Vector3.zero;
    private bool _airBorne;
    private float _fallTimer;

    void Start() {
        _health = GetComponent<Health>();
        _bac = GetComponent<BloodAlcohol>();
        _health.OnDeath.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        _combat = GetComponent<PlayerCombat>();
        _charController = GetComponent<CharacterController>();
    }

    private void Update() {
        if (Input.GetButtonDown(GameInput.Fire)) {
            _combat.Attack();
        }

        if (_charController.isGrounded) {
            if (_airBorne) {
                _airBorne = false;
                if (_fallTimer > fallAnimTime) {
                    print("trigger fall over");
                }
                _fallTimer = 0;
            }
            
            var input = new Vector3(Input.GetAxis(GameInput.Horizontal), 0, Input.GetAxis(GameInput.Vertical));
            var moveDir = Vector3.Lerp(input, _movement.normalized, _bac.Current * drunkMotion);
            _movement = moveDir * speed;

            if (_movement != Vector3.zero) {
                // Look in the direction of movement, or direction last moved in if standing still
                transform.LookAt(transform.position + (_movement.magnitude > 1 ? _movement : transform.forward));
            }
        } else  {
            if (!_airBorne) {
                _airBorne = true;
            }

            _fallTimer += Time.deltaTime;
        }

        _movement.y += Physics.gravity.y * Time.deltaTime;
        _charController.Move(_movement * speed * Time.deltaTime);
    }
}
