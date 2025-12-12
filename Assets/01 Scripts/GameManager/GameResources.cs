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

    #region GAME MANAGER
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
    public GameObject EnemyHPBarPrefab;
    #endregion

    #region DAMAGE NUMBER
    [Space(10)]
    [Header("DAMAGE NUMBER")]
    public Sprite[] DamageSprite;
    #endregion
}
