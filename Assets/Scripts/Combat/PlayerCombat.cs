using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public float punchReach;
    public int damage;

    [Header("Combo Settings")]
    public float timeout;
    public float[] minTimings;

    public bool IsPunching { get { return _chainIndex != 0; } }

    private int _chainIndex;
    private float _lastAttackTime;
    private Animator _animator;

    void Start() {
        _animator = GetComponentInChildren<Animator>();        
    }

    void Update () {
        _lastAttackTime = Mathf.Clamp(_lastAttackTime - Time.deltaTime, 0, 99999);
        if (Time.time - _lastAttackTime > timeout) {
            _chainIndex = 0;
        }
    }

    public void Attack() {
        if (Time.time - _lastAttackTime < minTimings[_chainIndex]) {
            // Can't attack now
            return;
        }

        GetComponent<MouseLook>().LookAtMouse();
        _animator.SetTrigger("punch");

        StartCoroutine(ThrowPunch(minTimings[_chainIndex]));
        // Punch # _chainIndex + 1 of combo
        _lastAttackTime = Time.time;
        _chainIndex++;

        if (_chainIndex == minTimings.Length) {
            _chainIndex = 0;
        }
    }
    
    private void DoDamage() {

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, punchReach)) {
            var hitObject = hit.collider.gameObject;
            var opponentHealth = hitObject.GetComponent<Health>();
            if (opponentHealth == null)
                return;

            opponentHealth.TakeDamage(damage);
        }
    }

    private IEnumerator ThrowPunch(float delay) {
        yield return new WaitForSeconds(delay);
        DoDamage();
    }
}
