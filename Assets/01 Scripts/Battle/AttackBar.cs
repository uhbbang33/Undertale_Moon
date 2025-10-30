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
    private WaitForSeconds waitForBlink;
    private Tween tween;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        waitForBlink = new WaitForSeconds(_blinkInterval);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        tween = transform.DOMoveX(35f, 1.5f)
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

            yield return waitForBlink;
        }

        _spriteRenderer.sprite = _originSprite;
        StopCoroutine(_curCoroutine);
        yield return null;
    }
    

    public void OnSubmit(BaseEventData eventData)
    {
        tween.Kill();

        // 공격 모션(위에서 아래로 공격)
        // 애니메이션?



        // 깜박거림
        _curCoroutine = StartCoroutine(BlinkRoutine());

        // 적에게 데미지


    }
}
