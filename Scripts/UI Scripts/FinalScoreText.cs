using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreText : MonoBehaviour
{
    public TMPro.TMP_Text text;
    private void Awake()
    {
        text.text = "Final Score: "+GameOptions.Instance.GetTotalScore();
    }
}
