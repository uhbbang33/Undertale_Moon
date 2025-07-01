using UnityEngine;

public class FrogHeadMove : MonoBehaviour
{
    [SerializeField] private float _movePeriod;
    [SerializeField] private float _moveXScale;
    [SerializeField] private float _moveYScale;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        float t = Time.time * (2 * Mathf.PI / _movePeriod);
        float x = Mathf.Sin(t);
        float y = Mathf.Sin(t * 2);

        transform.localPosition = _startPosition + new Vector3(x, 0, 0) * _moveXScale + new Vector3(0, y, 0) * _moveYScale;
    }
}
