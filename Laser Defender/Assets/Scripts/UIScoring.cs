using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoring : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(scoreText != null) //Display the high score(if on main menu and game over menu) and score(if on game over menu).
        {
            scoreText.text = "You Scored:\n" + scoreKeeper.GetScore().ToString("000000");
        }
        highScoreText.text = "High Score: " + scoreKeeper.GetHighScore().ToString("000000");
    }


}
