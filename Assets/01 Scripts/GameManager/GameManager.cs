using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private Player _player;
    public Player Player => _player;

    [SerializeField] private float _minEnemyAppearTime; 
    [SerializeField] private float _maxEnemyAppearTime;

    private float _enemyAppearTime;

    private LobbyCamera _lobbyCam;
    public LobbyCamera LobbyCam => _lobbyCam;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        GameObject playerObject = Instantiate(GameResources.Instance.PlayerPrefab, Vector3.zero, Quaternion.identity);

        _player = playerObject.GetComponent<Player>();

    }

    private void Start()
    {
        InitEnemyAppearTime();
    }

    public GameObject GetLobbyPlayer()
    {
        return _player.LobbyPlayer;
    }

    private void InitEnemyAppearTime()
    {
        _enemyAppearTime = Random.Range(_minEnemyAppearTime, _maxEnemyAppearTime);
    }

    public void ReduceEnemyAppearTime(float moveTime)
    {
        _enemyAppearTime -= moveTime;

        if (_enemyAppearTime <= 0f)
        {
            ChangeSceneLobbyToBattle();
            InitEnemyAppearTime();
        }
    }

    private void ChangeSceneLobbyToBattle()
    {
        //TODO: async로 하트 깜박이는 연출 후 화면전환
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
        _player.ChangePlayer(false);

        // temp
        StartCoroutine(ChangeSceneCoroutine());
    }

    private void ChangeSceneBattleToLobby()
    {
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
        _player.ChangePlayer(true);
    }

    IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(3f);

        ChangeSceneBattleToLobby();

        yield return null;
    }
}
