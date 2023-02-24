using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPlaces;

    GameObject chaserPrefab;
    GameObject shooterPrefab;
    Player player;

    private void Start()
    {
        chaserPrefab = Resources.Load<GameObject>("Prefabs/EnemyShipChaser");
        shooterPrefab = Resources.Load<GameObject>("Prefabs/EnemyShipShooter");
        player = GameManager.Instance.GetMainPlayer();
    }
    public IEnumerator Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int whereToSpawn = (int)Random.Range(0, spawnPlaces.Length - 1);
            while (GetDistanceFromPlayer(spawnPlaces[whereToSpawn]) < 20)
            {
                whereToSpawn = (int)Random.Range(0, spawnPlaces.Length - 1);
            }

            GameObject go;
            float enemyToSpawn = Random.Range(0, 10);
            switch (enemyToSpawn)
            {
                case > 7:
                    go = Instantiate(chaserPrefab);
                    go.transform.position = spawnPlaces[whereToSpawn].position;
                    break;
                default:
                    go = Instantiate(shooterPrefab);
                    go.transform.position = spawnPlaces[whereToSpawn].position;
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private float GetDistanceFromPlayer(Transform t)
    {
        return Mathf.Sqrt(Mathf.Pow(t.position.x - player.transform.position.x, 2) + Mathf.Pow(t.position.y - player.transform.position.y, 2));
    }
}
