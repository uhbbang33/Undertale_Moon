using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyPlayer;
    [SerializeField] private GameObject _heartPlayer;

    private PlayerDetailsSO _playerDetails;
    private Health _health;

    public GameObject LobbyPlayer => _lobbyPlayer;

    private void Awake()
    {
        Initialize();

        DontDestroyOnLoad(gameObject);
    }

    private void Initialize()
    {
        _playerDetails = GameResources.Instance.PlayerDetails;

        //_health.SetStartHealth(_playerDetails.MaxHealth);
    }

    public void ChangePlayer(bool isLobby)
    {
        InitPosition(isLobby);

        _lobbyPlayer.SetActive(isLobby);
        _heartPlayer.SetActive(!isLobby);
    }

    public void SaveLobbyPosition()
    {
        _playerDetails.LobbyPosition = transform.position;
    }

    private void InitPosition(bool isLobby)
    {
        if (isLobby)
            transform.position = _playerDetails.LobbyPosition;
        else
            transform.position = _playerDetails.BattlePosition;
    }
}
