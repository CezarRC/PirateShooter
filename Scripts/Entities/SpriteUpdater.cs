using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteUpdater : MonoBehaviour
{
    public GameObject[] spritesToChange;
    public SpriteConfig[] spriteConfigs;

    public void UpdateSprites(int indexSprite)
    {
        for (int i = 0; i < spritesToChange.Length; i++)
        {
            for (int j = 0; j < spriteConfigs.Length; j++)
            {
                if (spritesToChange[i].name == spriteConfigs[j].spriteName)
                {
                    spritesToChange[i].GetComponent<SpriteRenderer>().sprite = spriteConfigs[j].spriteSheet[indexSprite];
                }
            }
        }
    }
}
