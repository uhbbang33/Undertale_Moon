using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class LobbyScenePlayerMove : PlayerMove
{
    private Vector2 _lastDirection;
    private Animator _animator;
    private PlayerDetailsSO _playerDetails;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _playerDetails = GameResources.Instance.PlayerDetails;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        transform.position = _playerDetails.LobbyPosition;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_moveDirection != Vector2.zero)
        {
            _animator.SetBool("IsMoving", true);
            _lastDirection = _moveDirection;

            GameManager.Instance.ReduceEnemyAppearTime(Time.fixedDeltaTime);
        }
        else
            _animator.SetBool("IsMoving", false);

        _animator.SetFloat("MoveX", _moveDirection.x);
        _animator.SetFloat("MoveY", _moveDirection.y);
        _animator.SetFloat("StopX", _lastDirection.x);
        _animator.SetFloat("StopY", _lastDirection.y);
    }
}
