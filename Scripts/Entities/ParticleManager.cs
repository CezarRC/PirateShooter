using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleSelection
{
    EnemyExplosion,
    PlayerExplosion,
    CannonBallExplosion
}
public class ParticleManager : MonoBehaviour
{
    List<GameObject> particlePrefabs;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        particlePrefabs = new List<GameObject>();
        particlePrefabs.Add(Resources.Load<GameObject>("Prefabs/Particles/EnemyExplosion"));
        particlePrefabs.Add(Resources.Load<GameObject>("Prefabs/Particles/PlayerExplosion"));
        particlePrefabs.Add(Resources.Load<GameObject>("Prefabs/Particles/CannonBallExplosion"));
    }

    public void CreateParticle(ParticleSelection particle, Vector3 position)
    {
        GameObject go = Instantiate(particlePrefabs[(int)particle]);
        go.transform.position = position;
    }
}
