using System;
using System.Collections;
using UnityEngine;

public class FroggitSkillFrog : MonoBehaviour
{
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _jumpSprite;
    [SerializeField] private float _idleTime;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpPower;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private WaitForSeconds _waitForIdle;
    private WaitForSeconds _waitForJump;
    
    private readonly Vector2 _startPos = new Vector2(7f, -14.3f);

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _waitForIdle = new WaitForSeconds(_idleTime);
        _waitForJump = new WaitForSeconds(_jumpTime);
    }

    private void OnEnable()
    {
        _spriteRenderer.sprite = _idleSprite;
        transform.position = _startPos;

        StartCoroutine(JumpRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator JumpRoutine()
    {
        yield return _waitForIdle;

        _spriteRenderer.sprite = _jumpSprite;

        Vector3 dir = (BattleManager.Instance.PlayerPosition - transform.position).normalized;

        _rigidbody.AddForce(dir * _jumpPower);

        yield return _waitForJump;

        // 여기서 battle manager 함수 호출


        gameObject.SetActive(false);
    }
}
