using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class CommonAI : MonoBehaviour {

    private const float MOVEMENT_LOWER_BOUND = 2F;

    [SerializeField]
    private int _attackPower;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private float _sightRange;

    [SerializeField]
    [Range(MOVEMENT_LOWER_BOUND, 10f)]
    private float _movementSpeed = MOVEMENT_LOWER_BOUND;

    private StateBehaviour _currentState;
    private bool _cachedPlayerInSight;
    private Health _player;
    private bool _isAttacking;

    private enum StateBehaviour {
        Idle = 0,
        Attack = 1,
        Moving = 2
    }

	private void Start () {
        SwitchToIdle();
        GetComponent<Health>().OnDeath.AddListener(() => PerformDeathAnimation());

        // Might need to make manager to pass reference to all ai along with other stuff (pathing, game state, etc..)
        _player = GameObject.FindGameObjectWithTag(GameTags.Player).GetComponent<Health>(); 
    }

    private void PerformDeathAnimation() {

    }

    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(3f);
        _isAttacking = false;
    }

    private void SwitchToIdle() {
        _currentState = StateBehaviour.Idle;
    }

    private void SwitchToMoving() {
        _currentState = StateBehaviour.Moving;
    }

    private void SwitchToAttack() {
        _currentState = StateBehaviour.Attack;
        StartCoroutine(AttackCoroutine());
    }
	
    private void Idle() {
        if (_cachedPlayerInSight)
            SwitchToMoving();
    }

    private void Move() {
        if (!_cachedPlayerInSight) {
            SwitchToIdle();
            return;
        }

        if (WithinAttackRange()) {
            SwitchToAttack();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _movementSpeed);
    }

    private void Attack() {
        if (!WithinAttackRange() && !_isAttacking) {
            SwitchToMoving();
            return;
        }
    }

    private bool WithinAttackRange() {
        return Vector3.Distance(transform.position, _player.transform.position) <= _attackRange;
    }

    private Vector3? PlayerDirection {
        get {
            if (!_player)
                return null;

            return (transform.position - _player.transform.position).normalized;
        }
    }

    private bool PlayerInSight() {
        var dir = PlayerDirection;
        if (!dir.HasValue)
            return false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up + transform.forward, dir.Value);
        if (Physics.Raycast(ray, out hit, _sightRange)) {
            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
                return true;
        }

        return false;
    }

    private void LookAtPlayer() {
        if (_player == null)
            return;

        transform.LookAt(_player.transform);
    }

	private void Update () {

        _cachedPlayerInSight = PlayerInSight();

        if (_cachedPlayerInSight)
            LookAtPlayer();

        switch (_currentState) {
            case StateBehaviour.Idle: Idle();
                break;
            case StateBehaviour.Moving: Move();
                break;
            case StateBehaviour.Attack: Attack();
                break;

            default: break;
        }
	}
}
