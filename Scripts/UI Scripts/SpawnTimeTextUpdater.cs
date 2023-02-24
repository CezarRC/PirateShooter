using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimeTextUpdater : TextUpdater
{
    public override void UpdateText()
    {
        switch (slider.value)
        {
            case 1:
                text.text = "Every 30 Seconds";
                break;
            case 2:
                text.text = "Every 25 Seconds";
                break;
            case 3:
                text.text = "Every 20 Seconds";
                break;
            default:
                break;
        }
    }
}
