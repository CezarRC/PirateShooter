using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : Cannon
{
    public override GameObject GetCannonBall()
    {
        return Resources.Load<GameObject>("Prefabs/EnemyCannonBall");
    }
}
