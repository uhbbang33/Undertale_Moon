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


    #region
    [Space(10)]
    [Header("PLAYER")]
    public GameObject PlayerPrefab;
    public PlayerDetailsSO PlayerDetails;
    #endregion
}
