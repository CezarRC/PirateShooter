using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : Boat
{
    bool playerFound = false;
    Player player;
    bool alreadyIdling;
    Vector2 idleTarget;
    int points = 5;

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
        if (playerFound == true && GetDistanceFromPlayer() < 10)
        {
            FollowTarget(player.transform.position);
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



    private void FollowTarget(Vector2 target)
    {
        float angleToRotate = Vector2.SignedAngle(transform.up, target - new Vector2(transform.position.x, transform.position.y));

        switch (angleToRotate)
        {
            case > 0:
                Rotate(90);
                break;
            case < 0:
                Rotate(-90);
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
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "EnemyShooter" || collision.gameObject.tag == "EnemyChaser")
        {
            playerFound = false;
            alreadyIdling = false;
        }
        if (collision.gameObject.tag == "CannonBall")
        {
            TakeDamage(Random.Range(15, 30));
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "MainPlayer")
        {
            TakeDamage(GetHealth());
            points = 0;
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
