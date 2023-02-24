using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float shotForce = 50;
    public Transform bulletSpawnPosition;
    Quaternion originalRotation;
    float lastShot;
    float rechargeTime = 1.5f;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void AimAtTarget(Vector2 target)
    {
        float targetAngle = Vector2.SignedAngle(transform.right, target - new Vector2(transform.position.x, transform.position.y));
        transform.Rotate(transform.forward, targetAngle);
    }
    public void ResetAim()
    {
        transform.localRotation = originalRotation;
    }

    public void Shoot(Vector2 target)
    {
        if (Time.time < lastShot)
        {
            return;
        }
        lastShot = Time.time + rechargeTime;

        GameObject cannonBall = Instantiate(GetCannonBall());
        cannonBall.transform.position = bulletSpawnPosition.position;
        cannonBall.GetComponent<Rigidbody2D>().AddForce((target - new Vector2(bulletSpawnPosition.position.x, bulletSpawnPosition.position.y)) * shotForce);
    }
    public virtual GameObject GetCannonBall()
    {
        return Resources.Load<GameObject>("Prefabs/CannonBall");
    }
}
