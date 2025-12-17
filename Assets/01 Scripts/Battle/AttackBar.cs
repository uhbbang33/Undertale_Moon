using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class AttackBar : MonoBehaviour, ISubmitHandler
{
    [SerializeField] private Sprite _originSprite;
    [SerializeField] private Sprite _hitSprite;
    [SerializeField] private float _blinkInterval;
    [SerializeField] private int _blinkCount;

    private Button _btn;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _curCoroutine;
    private WaitForSeconds _waitForBlink;
    private Tween _tween;
    private float _originXPos;

    private readonly float _maxBonus = 2.2f;
    private readonly float _minBouns = 1.0f;

    public event Action<float> OnAttack;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _waitForBlink = new WaitForSeconds(_blinkInterval);
        _originXPos = transform.position.x;
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        _tween = transform.DOMoveX(35f, 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);

        _btn.interactable = true;
    }

    private void OnDisable()
    {
        StopCoroutine(_curCoroutine);
    }

    private IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < _blinkCount; ++i)
        {
            if(i % 2== 0)
                _spriteRenderer.sprite = _hitSprite;
            else
                _spriteRenderer.sprite = _originSprite;

            yield return _waitForBlink;
        }

        _spriteRenderer.sprite = _originSprite;

        BattleManager.Instance.ChangeBattleState(BattleState.ENEMY_ATTACK);
        StopCoroutine(_curCoroutine);

        yield return null;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _btn.interactable = false;

        _tween.Kill();

        BattleManager.Instance.PlayerAttack(CalculateBonusDamage());

        _curCoroutine = StartCoroutine(BlinkRoutine());

        //OnAttack?.Invoke(CalculateBonusDamage());
    }

    private float CalculateBonusDamage()
    {
        float x = Math.Abs(transform.position.x) / Math.Abs(_originXPos);

        return _maxBonus - (x * (_maxBonus - _minBouns));
    }
}
