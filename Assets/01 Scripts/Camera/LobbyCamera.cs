using Unity.Cinemachine;
using UnityEngine;

public class LobbyCamera : MonoBehaviour
{
    private CinemachineCamera _cam;

    private void Awake()
    {
        _cam = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
       SetFollowTarget(GameManager.Instance.GetLobbyPlayer().transform);
    }

    public void SetFollowTarget(Transform transform)
    {
        _cam.Follow = transform;
    }
}
