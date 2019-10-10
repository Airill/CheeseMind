using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System; 

public class ScoreWindow : MonoBehaviour
{

    double missionCof = 1f;
    double starsPoints;
    
    private void Awake()
    {
        SetScore();
    }
    void SetScore()
    {
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("line");

        starsPoints = -(Math.Pow((allLines.Length-3), 2)) + 1.2 * Scores.points;
        if (starsPoints < 0)
        {
            starsPoints = 0;
        }
        GameObject scoreText = GameObject.Find("PlayerStars");
        TMP_Text playerPoints = scoreText.GetComponent<TMP_Text>();
        playerPoints.text = (Math.Ceiling(starsPoints) + " x");
    }
}
