using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void UpdateGameTime(float t)
    {
        GameOptions.Instance.SetGameTime(t);
    }
    public void UpdateWaveTime(float t)
    {
        GameOptions.Instance.SetWaveTime(t);
    }
}
