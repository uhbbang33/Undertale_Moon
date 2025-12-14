using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private InputActions _inputActions;
    protected Vector2 _moveDirection;
    private Rigidbody2D _rb;

    [SerializeField] private float _moveSpeed;

    protected virtual void Awake()
    {
        _moveDirection = Vector2.zero;
        _rb = GetComponent<Rigidbody2D>();
        _inputActions = new InputActions();
    }

    protected virtual void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        transform.position = new Vector2(0f, -8f);
    }

    protected virtual void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;

        _inputActions.Disable();

        _moveDirection = Vector2.zero;
    }

    protected virtual void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);
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
