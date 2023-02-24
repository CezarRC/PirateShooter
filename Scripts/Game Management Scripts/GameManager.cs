using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ScoreManager scoreManager;
    private MainPlayer player;
    private Canvas canvas;
    private Camera mainCamera;
    private ParticleManager particleManager;
    private float startTime;
    private int currentWave;
    private int lastWave;
    private EnemySpawner spawner;
    private bool spawnedWave;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            startTime = Time.time;
            spawner = GetComponent<EnemySpawner>();
            scoreManager = GetComponent<ScoreManager>();
        }
    }

    public float GetTime()
    {
        return Time.time - startTime;
    }
    public float GetWave()
    {
        return currentWave;
    }

    private void Update()
    {
        ManageGame();
    }
    private void ManageGame()
    {
        if (GetTime() > GameOptions.Instance.GetGameTime() && GetMainPlayer().GetAlive())
        {
            GameOptions.Instance.SetTotalScore(scoreManager.GetScore());
            SceneManager.LoadScene("WinScene");
        }
        switch ((int)GetTime() / GameOptions.Instance.GetWaveTime())
        {
            case 1:
                currentWave = 2;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(5));
                    lastWave = currentWave;
                }
                break;
            case 2:
                currentWave = 3;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(5));
                    lastWave = currentWave;
                }
                break;
            case 3:
                currentWave = 4;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(5));
                    lastWave = currentWave;
                }
                break;
            case 4:
                currentWave = 5;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(5));
                    lastWave = currentWave;
                }
                break;
            case 5:
                currentWave = 6;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(8));
                    lastWave = currentWave;
                }
                break;
            case 6:
                currentWave = 7;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(8));
                    lastWave = currentWave;
                }
                break;
            case 7:
                currentWave = 8;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(8));
                    lastWave = currentWave;
                }
                break;
            case 8:
                currentWave = 9;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(13));
                    lastWave = currentWave;
                }
                break;
            case 9:
                break;
            default:
                if (currentWave > 1)
                {
                    break;
                }
                currentWave = 1;
                if (currentWave != lastWave)
                {
                    StartCoroutine(spawner.Spawn(5));
                    lastWave = currentWave;
                }
                break;
        }
    }

    public Canvas GetGameCanvas()
    {
        if (canvas != null)
        {
            return canvas;
        }
        canvas = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<Canvas>();
        return canvas;
    }
    public Player GetMainPlayer()
    {
        if (player != null)
        {
            return player;
        }
        player = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<MainPlayer>();
        return player;
    }
    public Camera GetMainCamera()
    {
        if (mainCamera != null)
        {
            return mainCamera;
        }
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        return mainCamera;
    }

    public ParticleManager GetParticleManager()
    {
        if (particleManager != null)
        {
            return particleManager;
        }
        particleManager = GetComponent<ParticleManager>();
        return particleManager;
    }

    public void GameOver()
    {
        GameOptions.Instance.SetTotalScore(scoreManager.GetScore());
        SceneManager.LoadScene("GameOverMenu");
    }
}
