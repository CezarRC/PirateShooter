using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TMPro.TMP_Text textTime;
    public TMPro.TMP_Text textWave;

    private void Update()
    {
        UpdateHUD();
    }
    public void UpdateHUD()
    {
        float minutes = (int)GameManager.Instance.GetTime() / 60;
        float sixtySeconds = (int)GameManager.Instance.GetTime() % 60;
        float tens = (int)sixtySeconds / 10;
        float seconds = (int)sixtySeconds % 10;
        textTime.text = minutes+":"+tens+seconds;
        textWave.text = ""+GameManager.Instance.GetWave();
    }
}
