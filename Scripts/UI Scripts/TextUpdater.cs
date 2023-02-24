using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public TMPro.TMP_Text text;
    protected Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public virtual void UpdateText() {}
}
