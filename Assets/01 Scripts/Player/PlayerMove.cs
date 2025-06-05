using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : Move
{
    private Vector2 _lastDirection;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_moveDirection != Vector2.zero)
        {
            _animator.SetBool("IsMoving", true);
            _lastDirection = _moveDirection;
        }
        else
            _animator.SetBool("IsMoving", false);

        _animator.SetFloat("MoveX", _moveDirection.x);
        _animator.SetFloat("MoveY", _moveDirection.y);
        _animator.SetFloat("StopX", _lastDirection.x);
        _animator.SetFloat("StopY", _lastDirection.y);
    }
}
