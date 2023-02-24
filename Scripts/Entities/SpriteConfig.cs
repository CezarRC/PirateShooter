using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpriteConfig", menuName = "ScriptableObjects/SpriteConfig", order = 1)]
public class SpriteConfig : ScriptableObject
{
    public string spriteName;
    public Sprite[] spriteSheet;
}