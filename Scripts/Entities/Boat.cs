using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boat : MonoBehaviour
{
    float health = 100;
    ShowHealthbar healthBarDisplayer;
    protected Rigidbody2D rigidbody2d;
    protected float boatSpeed = 3f;
    protected bool isAlive = true;

    protected virtual void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        healthBarDisplayer = GetComponent<ShowHealthbar>();
    }
    protected virtual void Update()
    {
        if (!isAlive)
        {
            return;
        }
        AvoidUnwantedPhysicsBehaviors();
        HealthChecker();
    }
    public bool GetAlive()
    {
        return isAlive;
    }
    public float GetHealth()
    {
        return health;
    }
    public virtual void Move()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + new Vector2(rigidbody2d.transform.up.x, rigidbody2d.transform.up.y) * boatSpeed * Time.fixedDeltaTime);
    }
    private void AvoidUnwantedPhysicsBehaviors()
    {
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.angularVelocity = 0;
    }
    public virtual void Idle() { }
    public virtual void Anchor() { }
    public void Rotate(float angle)
    {
        rigidbody2d.MoveRotation(rigidbody2d.rotation + angle * Time.fixedDeltaTime);
    }

    protected void TakeDamage(float amount)
    {
        health -= amount;
        healthBarDisplayer.SetHealthBarAmount(health);
        CheckUpdatedSprite();
    }
    protected void Heal(float amount)
    {
        health += amount;
        healthBarDisplayer.SetHealthBarAmount(health);
        CheckUpdatedSprite();
    }

    private void HealthChecker()
    {
        if (health > 0)
        {
            return;
        }
        CheckUpdatedSprite();
        Sink();
    }

    protected virtual void Sink()
    {
        isAlive = false;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        Destroy(gameObject, 3f);
    }
    protected void CheckUpdatedSprite()
    {
        switch (GetHealth())
        {
            case >= 75:
                GetComponent<SpriteUpdater>().UpdateSprites(0);
                break;
            case >= 50:
                GetComponent<SpriteUpdater>().UpdateSprites(1);
                break;
            case >= 25:
                GetComponent<SpriteUpdater>().UpdateSprites(2);
                break;
            case <= 0:
                GetComponent<SpriteUpdater>().UpdateSprites(3);
                break;
            default:
                break;
        }
    }
}
