using UnityEngine;
using UnityEngine.InputSystem;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;
    
    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    #region GAMEMANAGER
    [Space(10)]
    [Header("GAME MANAGER")]
    public GameObject GameManagerPrefab;
    #endregion

    #region PLAYER
    [Space(10)]
    [Header("PLAYER")]
    public GameObject PlayerPrefab;
    public PlayerDetailsSO PlayerDetails;
    #endregion

    #region ENEMY
    [Space(10)]
    [Header("ENEMY")]
    public EnemyDetailsSO FroggitDetails;
    #endregion
}
