using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreKeeper : MonoBehaviour, IDataPersistance

//Records the player's score, highscore, current wave, and play time.
{    int score = 0;
    int highScore = 0;
    float playTime = 0f;
    int playTimeInt = 0;
    bool stopTimer;
    static ScoreKeeper instance; 
    DataPersistanceManager dataPersistanceManager;

    void Awake()
    {
        ManageSingleton();
        dataPersistanceManager = FindObjectOfType<DataPersistanceManager>();
        LoadHighScore();
        DataPersistanceManager.instace.LoadGame();
        
    }

    public void LoadData(GameData data)
    {
        //Load dictionary of scores.
    }

    public void SaveData(GameData data)
    {
        //Save dictionary of scores.
    }


    void ManageSingleton() //Retains the same ScoreKeepr between scene changes so that the last score and highscore can be displayed.
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetPlayTime()
    {
        return playTimeInt;
        
    }

    private void Update() 
    {
        if(!stopTimer)
        {
            playTime += Time.deltaTime;
            playTimeInt = Mathf.RoundToInt(playTime);
        }

        if(score > highScore)
        {
            highScore = score;
        }
    }

    public void StopTimer()
    {
        stopTimer = true;
    }

    public void ResetTimer()
    {
        playTime = 0f;
        playTimeInt = 0;
        stopTimer = false;
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("High Score", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("High Score");
    }

}
