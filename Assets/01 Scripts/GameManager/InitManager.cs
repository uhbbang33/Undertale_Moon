using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    // TODO: Intro Scene

    private void Awake()
    {
        Instantiate(GameResources.Instance.GameManagerPrefab);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync("LobbyScene"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.completed += (asyncOp) =>
        { };

        yield return op;
    }
}
