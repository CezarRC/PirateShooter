using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    float shotTime;
    private void Awake()
    {
        shotTime = Time.time;
    }
    private void Update()
    {
        if (shotTime + 1.5f < Time.time)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground" || collision.collider.gameObject.tag == "Rock")
        {
            Destroy(gameObject);
        }
        if ((collision.collider.gameObject.tag == "Player" || 
            collision.collider.gameObject.tag == "EnemyChaser" || 
            collision.collider.gameObject.tag == "EnemyShooter" || 
            collision.collider.gameObject.tag == "EnemyCannonBall" || 
            collision.collider.gameObject.tag == "CannonBall") 
            && 
            collision.collider.gameObject.tag != gameObject.tag)
        {
            GameManager.Instance.GetParticleManager().CreateParticle(ParticleSelection.CannonBallExplosion, transform.position);
            Destroy(gameObject);
        }
    }
}
