using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aditya.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int spawnCount   = 5;
        [SerializeField] private float spawnHeight = 2f;

        public void SpawnEnemies(HashSet<Vector2Int> path)
        {
            if (path == null || path.Count == 0) return;

            var allCells = path.ToList();

            for (int i = 0; i < spawnCount; i++)
            {
                Vector2Int cell = allCells[Random.Range(0, allCells.Count)];

                Vector3 spawnPos = new Vector3(cell.x, spawnHeight, cell.y);
                Quaternion rot    = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                Instantiate(enemyPrefab, spawnPos, rot, transform);
            }
        }
    }
}