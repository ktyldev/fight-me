using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class CommonAI : MonoBehaviour {

    [SerializeField]
    private int _attackPower;

    private StateBehaviour _currentState;
    private Health _playerHealth;

    private enum StateBehaviour {
        Idle = 0,
        Attack = 1,
        Moving = 2
    }

	private void Start () {
        SwitchToIdle();
        GetComponent<Health>().OnDeath.AddListener(() => PerformDeathAnimation());

        // Might need to make manager to pass reference to all ai along with other stuff (pathing, game state, etc..)
        _playerHealth = GameObject.FindGameObjectWithTag(GameTags.Player).GetComponent<Health>(); 
    }

    private void PerformDeathAnimation()
    {

    }

    private void SwitchToIdle() {
        _currentState = StateBehaviour.Idle;
    }

    private void SwitchToMoving() {
        _currentState = StateBehaviour.Moving;
    }

    private void SwitchToAttack() {
        _currentState = StateBehaviour.Attack;
    }
	
    private void Idle() {

    }

    private void Move() {

    }

    private void Attack() {

    }

	// Update is called once per frame
	private void Update () {
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
