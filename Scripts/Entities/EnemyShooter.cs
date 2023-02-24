using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : WeaponizedBoat
{
    bool playerFound = false;
    Player player;
    bool alreadyIdling;
    Vector2 idleTarget;
    public EnemyCannon cannonLeft, cannonRight;
    float[] maxRightCannonRotation = { -160, 15 };
    float[] maxLeftCannonRotation = { -15, 160 };
    int points = 3;

    protected override void Start()
    {
        base.Start();
        boatSpeed = 2.0f;
    }
    protected override void Update()
    {
        if (!isAlive)
        {
            return;
        }
        base.Update();
        Behave();
    }
    private void Behave()
    {
        if (playerFound == true)
        {
            float distance = GetDistanceFromPlayer();
            switch (distance)
            {
                case > 10:
                    Idle();
                    break;
                case > 7:
                    FollowTarget(player.transform.position);
                    break;
                case > 5:
                    FollowTarget(player.transform.position);
                    Shoot(player.transform.position);
                    break;
                case <= 5:
                    Shoot(player.transform.position);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Idle();
        }
    }

    public override void Idle()
    {
        if (!alreadyIdling)
        {
            idleTarget = new Vector2(Random.Range(-8, 8), Random.Range(-8, 8));
            alreadyIdling = true;
        }
        FollowTarget(idleTarget);
    }

    public override void Shoot(Vector2 target)
    {
        float shootAngle = Vector2.SignedAngle(transform.up, target - new Vector2(transform.position.x, transform.position.y));
        if (shootAngle >= maxRightCannonRotation[0] && shootAngle <= maxRightCannonRotation[1])
        {
            cannonRight.AimAtTarget(target);
            cannonRight.Shoot(target);
        }
        if (shootAngle >= maxLeftCannonRotation[0] && shootAngle <= maxLeftCannonRotation[1])
        {
            cannonLeft.AimAtTarget(target);
            cannonLeft.Shoot(target);
        }
    }

    private void FollowTarget(Vector2 target)
    {
        float angleToRotate = Vector2.SignedAngle(transform.up, target - new Vector2(transform.position.x, transform.position.y));

        switch (angleToRotate)
        {
            case > 0:
                Rotate(45);
                break;
            case < 0:
                Rotate(-45);
                break;
            default:
                break;
        }
        if (angleToRotate < 10 && angleToRotate > -10)
        {
            Move();
        }
    }

    private float GetDistanceFromPlayer()
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "EnemyChaser" || collision.gameObject.tag == "EnemyShooter")
        {
            playerFound = false;
            alreadyIdling = false;
        }
        if (collision.gameObject.tag == "CannonBall")
        {
            TakeDamage(Random.Range(15, 30));
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyShooter" || collision.gameObject.tag == "EnemyChaser")
        {
            playerFound = false;
            alreadyIdling = false;
            rigidbody2d.MovePosition(rigidbody2d.position + new Vector2(0.2f, 0));
            collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(collision.gameObject.GetComponent<Rigidbody2D>().position + new Vector2(-0.2f, 0));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer")
        {
            player = collision.gameObject.GetComponent<MainPlayer>();
            playerFound = true;
        }
    }
    

    protected override void Sink()
    {
        base.Sink();
        GameManager.Instance.GetParticleManager().CreateParticle(ParticleSelection.EnemyExplosion, transform.position);
        GameManager.Instance.scoreManager.AddScore(points);
    }
}
