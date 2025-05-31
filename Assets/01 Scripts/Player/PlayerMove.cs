using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private InputActions _inputActions;
    private Vector2 _moveDirection;
    private Vector2 _lastDirection;
    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _moveDirection = Vector2.zero;
        _inputActions = new InputActions();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;

        _inputActions.Disable();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);

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

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>().normalized;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveDirection = Vector2.zero;
    }
}
