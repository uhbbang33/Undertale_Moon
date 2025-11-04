using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackBar : MonoBehaviour, ISubmitHandler
{
    [SerializeField] private Sprite _originSprite;
    [SerializeField] private Sprite _hitSprite;
    [SerializeField] private float _blinkInterval;
    [SerializeField] private int _blinkCount;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _curCoroutine;
    private WaitForSeconds _waitForBlink;
    private Tween _tween;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _waitForBlink = new WaitForSeconds(_blinkInterval);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        _tween = transform.DOMoveX(35f, 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
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
        StopCoroutine(_curCoroutine);
        yield return null;
    }
    

    public void OnSubmit(BaseEventData eventData)
    {
        _tween.Kill();

        BattleManager.Instance.EnemyHit();

        _curCoroutine = StartCoroutine(BlinkRoutine());

        // 적에게 데미지


    }
}
