using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationTextUpdater : TextUpdater
{
    public override void UpdateText()
    {
        switch (slider.value)
        {
            case 1:
                text.text = "1 Minute";
                break;
            default:
                text.text = slider.value + " Minutes";
                break;
        }
    }
}
