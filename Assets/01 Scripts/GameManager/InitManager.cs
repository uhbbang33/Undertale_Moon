using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    // TODO: Intro Scene
    [SerializeField] private GameObject GameManagerPrefab;


    private void Awake()
    {
        Instantiate(GameManagerPrefab);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync("LobbyScene"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.completed += (asyncOp) =>
        {

        };

        yield return op;
    }
}
