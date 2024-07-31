using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnEnemyPos;
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    public int currentEnemies;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        currentEnemies = spawnEnemyPos.Count;
        SpawnEnemy();
        SpawnPlayer();
    }

    private void SpawnEnemy()
    {
        foreach (Transform enemyPos in spawnEnemyPos)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if(enemyController != null)
            {
                enemyController.currentMap = transform.GetComponent<MapController>();
            }

            Vector3 spawnPos = enemyPos.position;
            spawnPos.y = 1f;
            enemy.transform.position = spawnPos;
        }
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, transform);
        Vector3 spawnPos = playerSpawnPos.position;
        spawnPos.y = 1f;
        player.transform.position = spawnPos;

        //Set target for camera
        CameraController.Instance.SetTarget(player);
    }
}
