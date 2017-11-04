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
    public float getUpTime;

    private CharacterController _charController;
    private Health _health;
    private BloodAlcohol _bac;
    private PlayerCombat _combat;
    private Animator _animator;
    private Vector3 _movement = Vector3.zero;
    private float _fallTimer;

    private bool _fallenOver;

    void Start() {
        _health = GetComponent<Health>();
        _bac = GetComponent<BloodAlcohol>();
        _health.OnDeath.AddListener(() => StartCoroutine(Die()));
        _combat = GetComponent<PlayerCombat>();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void Update() {
        if (Input.GetButtonDown(GameInput.Fire)) {
            _combat.Attack();
        }

        if (_charController.isGrounded) {
            if (_fallTimer != 0) {
                if (_fallTimer > fallAnimTime) {
                    FallOver();
                }
                _fallTimer = 0;
            }

            if (!_fallenOver) {
                HandleInput();
            }
        } else {
            _fallTimer += Time.deltaTime;
        }

        if (_combat.IsPunching || _bac.IsDrinking || _fallenOver)
            return;

        Move();
    }

    private void HandleInput() {
        var input = new Vector3(Input.GetAxis(GameInput.Horizontal), 0, Input.GetAxis(GameInput.Vertical));
        var moveDir = Vector3.Lerp(input, _movement.normalized, _bac.Current * drunkMotion);
        _movement = moveDir * speed;

        if (_movement != Vector3.zero) {
            // Look in the direction of movement, or direction last moved in if standing still
            transform.LookAt(transform.position + (_movement.magnitude > 1 ? _movement : transform.forward));
        }
    }

    private void Move() {
        _animator.SetBool(GameTags.anim_moving, _charController.velocity != Vector3.zero);
        _movement.y += Physics.gravity.y * Time.deltaTime;
        _charController.Move(_movement * speed * Time.deltaTime);
    }

    private void FallOver() {
        _fallenOver = true;
        _animator.SetTrigger(GameTags.anim_fall_over);
        StartCoroutine(GetUp());
    }

    private IEnumerator Die() {
        _fallenOver = true;
        _animator.SetBool(GameTags.anim_moving, false);
        _animator.SetTrigger(GameTags.anim_die);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private IEnumerator GetUp() {
        yield return new WaitForSeconds(getUpTime);
        _animator.SetTrigger(GameTags.anim_get_up);
        _fallenOver = false;
    }
}
