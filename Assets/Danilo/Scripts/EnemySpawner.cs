using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemyCount = 3;
        [SerializeField] private Vector3 spawnAreaSize = new Vector3(10, 0, 10);
        [SerializeField] private float groundY = 0f; // Y position for ground height

        private List<GameObject> spawnedEnemies = new List<GameObject>();

        private void Start()
        {
            // spawn enemies when the game starts
            SpawnEnemies();
        }

        // spawns the enemies at random positions
        private void SpawnEnemies()
        {
            // making sure the enemy prefab is assigned
            if (enemyPrefab == null)
            {
                Debug.LogError("there is no enemy prefab");
                return;
            }

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(enemy);
                Debug.Log($"the enemy spawned at {spawnPosition}");
            }
        }

        // creates random spawn positions within the spawn area
        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                0f,
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            randomPos.y = groundY; // Force spawn at correct ground height
            return randomPos;
        }

        // draws a gizmo in the editor to show the spawn area
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, spawnAreaSize);
        }
    }
}
