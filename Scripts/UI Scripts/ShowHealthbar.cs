using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealthbar : MonoBehaviour
{
    GameObject healthBar;
    Canvas gameCanvas;
    Camera mainCamera;
    float healthBarAmount = 1;
    public void CreateHealthBar()
    {
        gameCanvas = GameManager.Instance.GetGameCanvas();
        mainCamera = GameManager.Instance.GetMainCamera();
        healthBar = Instantiate(Resources.Load<GameObject>("Prefabs/HealthBar"));
        healthBar.transform.SetParent(gameCanvas.transform);
        healthBar.transform.localScale = Vector3.one;
    }
    public void UpdateHealthBar()
    {
        Vector3 positionOnViewport = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        Vector2 canvasSize = gameCanvas.pixelRect.size;
        healthBar.transform.position = positionOnViewport * canvasSize + new Vector2(0, 60);
        healthBar.GetComponent<HealthBar>().SetFillAmount(healthBarAmount);
    }
    private void Awake()
    {
        CreateHealthBar();
    }

    private Boat GetBoatComponent()
    {
        switch (gameObject.tag)
        {
            case "MainPlayer":
                return GetComponent<MainPlayer>();
            case "EnemyChaser":
                return GetComponent<EnemyChaser>();
            case "EnemyShooter":
                return GetComponent<EnemyShooter>();
            default:
                return new Boat();
        }

    }
    private void Update()
    {
        UpdateHealthBar();
    }
    private void OnDestroy()
    {
        Destroy(healthBar);
    }
    public void SetHealthBarAmount(float amount)
    {
        healthBarAmount = amount / 100;
    }
}
