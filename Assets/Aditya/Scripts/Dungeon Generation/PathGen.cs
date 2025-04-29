using System.Collections.Generic;
using System.Linq;
using Aditya.Scripts.Enemies;
using UnityEngine;

namespace Aditya.Scripts.Dungeon_Generation
{
 public class PathGen : MonoBehaviour
    {
        [SerializeField] private GameObject floorPrefab;
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private int stepSize = 50;
        [SerializeField] private int complexity = 3;
        [SerializeField] private Vector2Int startPosition = Vector2Int.zero;

        private readonly List<GameObject> _instantiatedObjects = new List<GameObject>();
        private EnemySpawner _enemySpawner;

        private void Start()
        {
            _enemySpawner = GetComponent<EnemySpawner>();
            MakeNewDungeon();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClearDungeon();
                MakeNewDungeon();
            }
        }

        private void MakeNewDungeon()
        {
            // Choose simple or complex walker
            RandomWalk walker = complexity <= 1 
                ? new RandomWalk(stepSize)
                : new IterativeRandomWalk(stepSize, complexity);

            HashSet<Vector2Int> path = walker.GenerateWalk(startPosition);

            // Spawn floors
            foreach (var cell in path)
            {
                Vector3 worldPos = new Vector3(cell.x, 0f, cell.y);
                GameObject floor = Instantiate(floorPrefab, worldPos, Quaternion.identity, transform);
                _instantiatedObjects.Add(floor);
            }

            // Spawn walls around path
            var wallPositions = GetWallPositions(path);
            foreach (var cell in wallPositions)
            {
                Vector3 worldPos = new Vector3(cell.x, 0f, cell.y);
                GameObject wall = Instantiate(wallPrefab, worldPos, Quaternion.identity, transform);
                _instantiatedObjects.Add(wall);
            }
            
            // Spawn enemies
            if (_enemySpawner != null)
            {
                _enemySpawner.SpawnEnemies(path);
            }
        }

        private void ClearDungeon()
        {
            foreach (var obj in _instantiatedObjects)
            {
                Destroy(obj);
            }
            _instantiatedObjects.Clear();
        }

        private HashSet<Vector2Int> GetWallPositions(HashSet<Vector2Int> path)
        {
            var walls = new HashSet<Vector2Int>();

            foreach (var group in path.GroupBy(cell => cell.x))
            {
                int x = group.Key;
                int minY = group.Min(cell => cell.y);
                int maxY = group.Max(cell => cell.y);
                walls.Add(new Vector2Int(x, minY - 1));
                walls.Add(new Vector2Int(x, maxY + 1));
            }

            foreach (var group in path.GroupBy(cell => cell.y))
            {
                int y = group.Key;
                int minX = group.Min(cell => cell.x);
                int maxX = group.Max(cell => cell.x);
                walls.Add(new Vector2Int(minX - 1, y));
                walls.Add(new Vector2Int(maxX + 1, y));
            }

            walls.ExceptWith(path);
            return walls;
        }
    }
}
