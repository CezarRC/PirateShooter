using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions Instance { get; private set; }

    float gameTime = 60;
    float waveTime = 30;
    string currentScene;
    int totalScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void SetGameTime(float t)
    {
        gameTime = t*60;
    }
    public void SetWaveTime(float t)
    {
        switch (t)
        {
            case 1:
                waveTime = 30;
                break;
            case 2:
                waveTime = 25;
                break;
            case 3:
                waveTime = 20;
                break;
            default:
                break;
        }
    }
    public float GetGameTime()
    {
        return gameTime;
    }
    public float GetWaveTime()
    {
        return waveTime;
    }
    public void SetTotalScore(int score)
    {
        totalScore = score;
    }
    public int GetTotalScore()
    {
        return totalScore;
    }
}
