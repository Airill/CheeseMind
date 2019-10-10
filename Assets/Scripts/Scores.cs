using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{

    public static int points = 0;
    public static int cheesePoints = 1;

    public static void AddScores(int score)
    {
        points += score;
    }

    public static void RestartScores()
    {
        points = 0;
    }


}