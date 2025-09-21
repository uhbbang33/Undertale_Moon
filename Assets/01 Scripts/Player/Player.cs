using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyPlayer;
    public GameObject LobbyPlayer => _lobbyPlayer;

    [SerializeField] private GameObject _heartPlayer;

    private PlayerInput _lobbyInput;

    private PlayerDetailsSO _playerDetails;
    private Health _health;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _playerDetails = GameResources.Instance.PlayerDetails;

        _lobbyInput = _lobbyPlayer.GetComponent<PlayerInput>();
    }

    public void ChangePlayer(bool isLobby)
    {
        _lobbyPlayer.SetActive(isLobby);
        _heartPlayer.SetActive(!isLobby);

    }

    private void Initialize(PlayerDetailsSO playerDetails)
    {
        _health.SetStartHealth(_playerDetails.MaxHealth);
    }
}
