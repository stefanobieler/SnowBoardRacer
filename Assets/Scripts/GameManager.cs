using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private int count = 0;

    void Awake()
    {
        if (gm != null)
        {
            Destroy(gameObject);
            return;
        }

        gm = this;
        DontDestroyOnLoad(this);
    }
    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        FinishLine.playerCrossedFinishLine += OnPlayerCrossedFinishLine;
        SnowBoardController.PlayerFell += OnPlayerFell;
    }

    private void OnPlayerFell()
    {
        SnowBoardController.PlayerFell -= OnPlayerFell;
        StartCoroutine(SlowReload());
    }

    private void OnPlayerCrossedFinishLine()
    {
        FinishLine.playerCrossedFinishLine -= OnPlayerCrossedFinishLine;
        StartCoroutine(SlowReload());
    }

    private IEnumerator SlowReload(float delay = 1)
    {
        yield return new WaitForSeconds(delay);
        ReloadLevel();
    }

    private void ReloadLevel()
    {

        SceneManager.LoadScene(0);
    }
}
