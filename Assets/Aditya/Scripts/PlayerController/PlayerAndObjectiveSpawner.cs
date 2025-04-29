using System.Collections.Generic;
using System.Linq;             // for Min/Max and First()
using UnityEngine;

namespace Aditya.Scripts.PlayerController
{
    public class PlayerAndObjectiveSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject objectivePrefab;
        [SerializeField] private Vector3 spawnOffset = new Vector3(0, 1f, 0);

        public void SpawnAtEnds(HashSet<Vector2Int> path)
        {
            if (path == null || path.Count == 0)
            {
                Debug.Log("No Path");
                return;
            }

            int minX = path.Min(cell => cell.x);
            int maxX = path.Max(cell => cell.x);

            Vector2Int leftMost = path.First(cell => cell.x == minX);
            Vector2Int rightMost = path.First(cell => cell.x == maxX);

            Vector3 playerPos = new Vector3(leftMost.x, 0f, leftMost.y) + spawnOffset;
            Vector3 objectivePos = new Vector3(rightMost.x, 0f, rightMost.y) + spawnOffset;

            Instantiate(playerPrefab, playerPos, Quaternion.identity);
            Instantiate(objectivePrefab, objectivePos, Quaternion.identity);
        }
    }
}