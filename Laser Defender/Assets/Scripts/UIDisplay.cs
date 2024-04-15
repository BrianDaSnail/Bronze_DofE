using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] UnityEngine.UI.Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score and Playtime")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI playTimeText;
    ScoreKeeper scoreKeeper;

    [Header("Wave Number")]
    [SerializeField] TextMeshProUGUI waveNumberText;
    [SerializeField] EnemySpawner enemySpawner;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    void Update() //Display the player's health, score, play time, and wave.
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000");
        playTimeText.text = scoreKeeper.GetPlayTime().ToString("000000");
        waveNumberText.text = enemySpawner.GetWaveNumber().ToString();

    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }


}
