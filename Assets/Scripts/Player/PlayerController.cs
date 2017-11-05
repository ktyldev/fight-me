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
    public Bounds movementBounds;

    private CharacterController _charController;
    private Health _health;
    private BloodAlcohol _bac;
    private PlayerCombat _combat;
    private Animator _animator;
    private Vector3 _movement = Vector3.zero;
    private SfxManager _sfx;
    private float _fallTimer;
    private bool _fallenOver;

    void Start() {
        _health = GetComponent<Health>();
        _bac = GetComponent<BloodAlcohol>();
        _combat = GetComponent<PlayerCombat>();
        _charController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _sfx = GameObject.FindGameObjectWithTag(GameTags.Music).GetComponent<SfxManager>();
        _health.OnDeath.AddListener(Die);
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

        if (Input.GetKeyDown(KeyCode.B))
            Application.Quit();
    }

    private void HandleInput() {
        var input = new Vector3(Input.GetAxis(GameInput.Horizontal), 0, Input.GetAxis(GameInput.Vertical));
        _movement = input * speed;

        if (_movement != Vector3.zero) {
            // Look in the direction of movement, or direction last moved in if standing still
            transform.LookAt(transform.position + _movement.normalized);
        }
    }

    bool _moving;
    private void Move() {
        _animator.SetBool(GameTags.anim_moving, _charController.velocity != Vector3.zero);
        _movement.y += Physics.gravity.y * Time.deltaTime;
        if (!_moving && _charController.velocity != Vector3.zero) {
            _moving = true;
            StartCoroutine(TakeSteps());
        }
        _charController.Move(_movement * speed * Time.deltaTime);
        transform.position = movementBounds.ClosestPoint(transform.position);
    }

    
    private IEnumerator TakeSteps() {
        while (_charController.velocity != Vector3.zero) {
            yield return new WaitForSeconds(0.47f);
            _sfx.Step();
        }

        _moving = false;
    }

    private void FallOver() {
        _fallenOver = true;
        _animator.SetTrigger(GameTags.anim_fall_over);
        StartCoroutine(GetUp());
    }

    private void Die() {
        _animator.SetTrigger(GameTags.anim_die);
        _animator.SetBool(GameTags.anim_moving, false);
        _fallenOver = true;
        GameOverScreen.GameOver(false);
    }

    private IEnumerator GetUp() {
        _animator.SetTrigger(GameTags.anim_get_up);
        yield return new WaitForSeconds(getUpTime);
        _fallenOver = false;
    }
}
