using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int scorePoints = 0;

    public void AddScore(int score)
    {
        scorePoints += score;
    }

    public int GetScore()
    {
        return scorePoints;
    }

    public int GetFinalScore()
    {
        return GameOptions.Instance.GetTotalScore();
    }
}
