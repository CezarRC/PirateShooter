using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : WeaponizedBoat
{
    private float timeStartedSailing;
    Vector2 playerInput;
    public PlayerCannon frontCannon;
    public PlayerCannon[] leftCannons;
    public PlayerCannon[] rightCannons;

    float[] maxFrontAngles = {-20, 20};
    float[] maxSideAngles = {60, 120};

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Sail();
        }
    }
    
    public override void Idle()
    {
        boatSpeed = Mathf.Lerp(boatSpeed, 0, 0.5f * Time.deltaTime);
        Move();
    }
    public override void Anchor()
    {
        boatSpeed = Mathf.Lerp(boatSpeed, 0, 1.5f * Time.deltaTime);
        Move();
    }

    private void OnShoot(InputValue inputValue)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 targetPosition = GameManager.Instance.GetMainCamera().ScreenToWorldPoint(mousePosition);
        Shoot(targetPosition);
    }

    private void OnAim()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 target = GameManager.Instance.GetMainCamera().ScreenToWorldPoint(mousePosition);
        float shotAngle = Vector2.SignedAngle(transform.up, target - new Vector2(transform.position.x, transform.position.y));
        
        if (shotAngle > maxFrontAngles[0] && shotAngle < maxFrontAngles[1])
        {
            frontCannon.AimAtTarget(target);
            return;
        }
        else
        {
            frontCannon.ResetAim();
        }
        if (shotAngle > -maxSideAngles[1] && shotAngle < -maxSideAngles[0])
        {
            foreach (Cannon cannon in rightCannons)
            {
                cannon.AimAtTarget(target);
            }
            return;
        }
        else
        {
            foreach (Cannon cannon in rightCannons)
            {
                cannon.ResetAim();
            }
        }
        if (shotAngle > maxSideAngles[0] && shotAngle < maxSideAngles[1])
        {
            foreach (Cannon cannon in leftCannons)
            {
                cannon.AimAtTarget(target);
            }
            return;
        }
        else
        {
            foreach (Cannon cannon in leftCannons)
            {
                cannon.ResetAim();
            }
        }
    }
    public override void Shoot(Vector2 target)
    {

        float shotAngle = Vector2.SignedAngle(transform.up, target - new Vector2(transform.position.x, transform.position.y));
        if (shotAngle > maxFrontAngles[0] && shotAngle < maxFrontAngles[1])
        {
            frontCannon.Shoot(target);
            return;
        }
        if (shotAngle > -maxSideAngles[1] && shotAngle < -maxSideAngles[0])
        {
            foreach (Cannon cannon in rightCannons)
            {
                cannon.Shoot(target);
            }  
            return;
        }
        if (shotAngle > maxSideAngles[0] && shotAngle < maxSideAngles[1])
        {
            foreach (Cannon cannon in leftCannons)
            {
                cannon.Shoot(target);
            }
            return;
        }
        
    }

    private void OnSail(InputValue inputValue)
    {
        playerInput = inputValue.Get<Vector2>();
    }

    private void Sail()
    {
        Debug.Log(boatSpeed);
        switch (playerInput.y)
        {
            case > 0f:
                boatSpeed = Mathf.Lerp(boatSpeed, UpdateBoatSpeed(), 2f*Time.deltaTime);
                Move();
                break;
            case < -0.2f:
                Anchor();
                break;
            default:
                Idle();
                break;
        }

        switch (playerInput.x)
        {
            case > 0:
                Rotate(-45);
                break;
            case < 0:
                Rotate(45);
                break;
            default:
                break;
        }
    }
    private void OnSailForward()
    {
        timeStartedSailing = Time.time;
    }
    private void OnSailIdle()
    {
    }

    private float UpdateBoatSpeed()
    {
        float SailTime = Time.time - timeStartedSailing;
        switch (SailTime)
        {
            case < 0:
                return 2.0f;
            case >= 4:
                return 5.0f;
            case >= 2:
                return 3.0f;
            default:
                return 2.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyChaser")
        {
            float damageAmount = Random.Range(25, 40);
            TakeDamage(damageAmount);
        }
        if (collision.gameObject.tag == "EnemyCannonBall")
        {
            TakeDamage(Random.Range(15, 30));
            Destroy(collision.gameObject);
        }
    }

    protected override void Sink()
    {
        base.Sink();
        GameManager.Instance.GetParticleManager().CreateParticle(ParticleSelection.PlayerExplosion,transform.position);
        GameManager.Instance.GameOver();
    }
}
