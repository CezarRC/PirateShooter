using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float CameraDistance = -10;
    private void Update()
    {
        Player mainPlayer = GameManager.Instance.GetMainPlayer();
        transform.position = new Vector3(mainPlayer.transform.position.x, mainPlayer.transform.position.y, CameraDistance);
    }
}
