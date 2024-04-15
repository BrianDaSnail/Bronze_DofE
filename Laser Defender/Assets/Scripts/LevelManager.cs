using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float loadGameOverDelay = 1f;
    [SerializeField] float loadMainLevelDelay = 0f;
    [SerializeField] float loadMainMenuDelay = 0f;
    [SerializeField] float loadLeaderboardDelay = 0f;
    ScoreKeeper scoreKeeper;
    DataPersistanceManager dataPersistanceManager;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        dataPersistanceManager = FindObjectOfType<DataPersistanceManager>();
    }

    public void LoadMainLevel() //Load the main level, reset the score, reset the timer.
    {
        scoreKeeper.ResetScore();
        scoreKeeper.ResetTimer();
        StartCoroutine(WaitAndLoad("MainLevel", loadMainLevelDelay));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(WaitAndLoad("MainMenu", loadMainMenuDelay));
    }

    public void LoadLeaderboard()
    {
        StartCoroutine(WaitAndLoad("Leaderboard", loadLeaderboardDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        scoreKeeper.SaveHighScore();
        DataPersistanceManager.instace.SaveGame();
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneName);

    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void LoadGameOver()
    {

        StartCoroutine(WaitAndLoad("GameOver", loadGameOverDelay));
    }
    
}
