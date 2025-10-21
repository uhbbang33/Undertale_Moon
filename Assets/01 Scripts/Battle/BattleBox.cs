using UnityEngine;
using DG.Tweening;

public class BattleBox : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private Transform _body;

    [Header("LINE")]
    [SerializeField] private Transform _topLine;
    [SerializeField] private Transform _bottomLine;
    [SerializeField] private Transform _leftLine;
    [SerializeField] private Transform _rightLine;

    [SerializeField] private float _lineThickness;

    [Header("INITIAL")]
    [SerializeField] private float _initialWidth;
    [SerializeField] private float _initialHeight;

    [Header("OFFSET")]
    [SerializeField] private float _lineWidthOffest;
    [SerializeField] private float _lineHeightOffest;

    [SerializeField] private float _animationDuration;

    private float _currentWidth;
    private float _currentHeight;
    private Vector2 _currentBodyPosition;

    void Awake()
    {
        _currentWidth = _initialWidth;
        _currentHeight = _initialHeight;
        _currentBodyPosition = _body.localPosition;
    }

    private void Start()
    {
        UpdateWidthScale(_currentWidth);
        UpdateHeightScale(_currentHeight);
    }

    public void ChangeWidth(float targetWidth)
    {
        DOTween.To(() => _currentWidth,
            x =>
            {
                _currentWidth = x;
                UpdateWidthScale(_currentWidth);
            }
            , targetWidth, _animationDuration).SetEase(Ease.Linear);
    }

    public void ChangeHeight(float targetHeight)
    {
        DOTween.To(() => _currentHeight,
           x =>
           {
               _currentHeight = x;
               UpdateHeightScale(_currentHeight);
           }
           , targetHeight, _animationDuration).SetEase(Ease.Linear);
    }

    public void Move(float targetX, float targetY)
    {
        DOTween.To(() => _currentBodyPosition,
            x =>
            {
                _currentBodyPosition = x;
                UpdatePosition(_currentBodyPosition);
            }, new Vector2(targetX, targetY), _animationDuration).SetEase(Ease.Linear);
    }

    private void UpdateWidthScale(float width)
    {
        // scale
        _body.localScale = new Vector3(width, _body.localScale.y, 1);

        _topLine.localScale = new Vector3(width + _lineWidthOffest, _lineThickness, 1);
        _bottomLine.localScale = new Vector3(width + _lineWidthOffest, _lineThickness, 1);

        // move positon
        _leftLine.localPosition = new Vector3(-width / 2, _leftLine.localPosition.y, 0);
        _rightLine.localPosition = new Vector3(width / 2, _rightLine.localPosition.y, 0);
    }

    private void UpdateHeightScale(float height)
    {
        // scale
        _body.localScale = new Vector3(_body.localScale.x, height, 1);

        _leftLine.localScale = new Vector3(_lineThickness, height + _lineHeightOffest, 1);
        _rightLine.localScale = new Vector3(_lineThickness, height + _lineHeightOffest, 1);

        // move position
        _topLine.localPosition = new Vector3(_topLine.localPosition.x, height / 2, 0);
        _bottomLine.localPosition = new Vector3(_bottomLine.localPosition.x, -height / 2, 0);
    }

    private void UpdatePosition(Vector2 pos)
    {
        _body.localPosition = pos;

        _topLine.localPosition = pos + new Vector2(0, (_leftLine.localScale.y - _lineHeightOffest) / 2);
        _bottomLine.localPosition = pos - new Vector2(0, (_leftLine.localScale.y - _lineHeightOffest) / 2);
        _leftLine.localPosition = pos + new Vector2((_topLine.localScale.x - _lineWidthOffest) / 2, 0);
        _rightLine.localPosition = pos - new Vector2((_topLine.localScale.x - _lineWidthOffest) / 2, 0);
    }
}
