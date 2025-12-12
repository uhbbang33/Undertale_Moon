using System;
using System.Collections;
using UnityEngine;

public class FroggitHeadMove : MonoBehaviour
{
    [SerializeField] private float _movePeriod;
    [SerializeField] private float _moveXScale;
    [SerializeField] private float _moveYScale;
    [SerializeField] private float _hitStopTime = 2f;

    private Vector3 _startPosition;
    private Animator _anim;
    private Enemy _enemy;
    private WaitForSeconds _waitForHit;
    private bool IsAttacked = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _startPosition = transform.localPosition;
        _waitForHit = new WaitForSeconds(_hitStopTime);
    }

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _enemy.OnHit += Hit;
    }

    void Update()
    {
        if (!IsAttacked)
            Move();
    }

    private void Move()
    {
        float t = Time.time * (2 * Mathf.PI / _movePeriod);
        float x = Mathf.Sin(t);
        float y = Mathf.Sin(t * 2);

        transform.localPosition = _startPosition + new Vector3(x, 0, 0) * _moveXScale + new Vector3(0, y, 0) * _moveYScale;
    }

    private void Hit()
    {
        StartCoroutine(HitRoutine());
    }

    IEnumerator HitRoutine()
    {
        IsAttacked = true;
        _anim.SetBool("Hit", true);

        transform.localPosition = _startPosition;

        yield return _waitForHit;

        IsAttacked = false;
        _anim.SetBool("Hit", false);
    }
}
