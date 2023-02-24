using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    public void SetFillAmount(float amount)
    {

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, amount, 0.3f);
    }
}
