using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Class responsible to spawn the enemies
public class SpawnEnemies : MonoBehaviour
{
    private ObjectPooling enemyChaserPool;
    private ObjectPooling enemyShooterPool;

    private float timeToSpawn;
    private float maxTimeToSpawn = 1;

    private Vector3Int center = Vector3Int.zero;
    [SerializeField] private Vector2 size;

    private Camera cam;
    private Tilemap obstacleGrid;

    Vector3Int randomPos;
    void Start()
    {
        maxTimeToSpawn = GameManager.Instance.InformationSaved.spawnTime;
        obstacleGrid = GameObject.FindGameObjectWithTag("Obstacles").GetComponent<Tilemap>();
        cam = Camera.main;

        GetEnemiesPoolings();

        StartCoroutine(SpawnRoutine());
    }

    private void GetEnemiesPoolings()
    {
        for (int i = 0; i < GameController.instance.poolings.Count; i++)
        {
            if (GameController.instance.poolings[i].GetPooledObj().GetComponent<EnemyChaserMovement>())
            {
                enemyChaserPool = GameController.instance.poolings[i];
            }
            else if (GameController.instance.poolings[i].GetPooledObj().GetComponent<EnemyShooterMovement>())
            {
                enemyShooterPool = GameController.instance.poolings[i];
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            timeToSpawn += Time.deltaTime;
            if(timeToSpawn >= maxTimeToSpawn)
            {
                int randomSpawn = Random.Range(0, 2);
                if(randomSpawn == 0)
                    SpawnEnemyChaser();
                else
                    SpawnEnemyShooter();

                timeToSpawn = 0;
            }
            yield return null;
        }
    }

    private void SpawnEnemyChaser()
    {
        GameObject enemyChaser = enemyChaserPool.GetPooledObj();

        SpawnEnemy(enemyChaser);
    }

    private void SpawnEnemyShooter()
    {
        GameObject enemyShooter = enemyShooterPool.GetPooledObj();

        SpawnEnemy(enemyShooter);
    }

    void SpawnEnemy(GameObject enemy)
    {
        Hp enemyHP = enemy.GetComponent<Hp>();
        enemyHP.ResetHP();

        randomPos = new Vector3Int((int)Random.Range(-size.x, size.x), (int)Random.Range(-size.y, size.y), 0);
        enemy.transform.position = center + randomPos;
        enemy.SetActive(true);

        CheckEnemiesSpawnOnSight(enemy);

        enemy.transform.position = center + randomPos;
    }

    private void CheckEnemiesSpawnOnSight(GameObject enemy)
    {
        Vector3 camSeen = cam.WorldToViewportPoint(enemy.transform.position);

        while (camSeen.x > 0 && camSeen.x < 1 && camSeen.y > 0 && camSeen.y < 1)
        {
            randomPos = new Vector3Int((int)Random.Range(-size.x, size.x), (int)Random.Range(-size.y, size.y), 0);

            if (obstacleGrid.GetTile(randomPos) != null)
            {
                randomPos = new Vector3Int((int)Random.Range(-size.x, size.x), (int)Random.Range(-size.y, size.y), 0);
            }
            enemy.transform.position = center + randomPos;
            camSeen = cam.WorldToViewportPoint(enemy.transform.position);
        }
    }
}
