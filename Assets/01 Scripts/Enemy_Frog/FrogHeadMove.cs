using UnityEngine;

public class FrogHeadMove : MonoBehaviour
{
    [SerializeField] private float movePeriod;
    [SerializeField] private float moveXScale;
    [SerializeField] private float moveYScale;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float t = Time.time * (2 * Mathf.PI / movePeriod);
        float x = Mathf.Sin(t);
        float y = Mathf.Sin(t * 2);

        transform.localPosition = startPosition + new Vector3(x, 0, 0) * moveXScale + new Vector3(0, y, 0) * moveYScale;
    }
}
