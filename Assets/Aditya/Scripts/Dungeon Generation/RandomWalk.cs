using System.Collections.Generic;
using UnityEngine;

namespace Aditya.Scripts.Dungeon_Generation
{
    public class RandomWalk
    {
        protected readonly int _steps;
        protected static readonly List<Vector2Int> Directions = new List<Vector2Int>
        {
            new(0, 1), // Up
            new(1, 0), // Right
            new(0, -1), // Down
            new(-1, 0) // Left
        };

        public RandomWalk(int steps)
        {
            _steps = steps;
        }

        public virtual HashSet<Vector2Int> GenerateWalk(Vector2Int startPosition)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
            var currentPos = startPosition;
            for (int i = 0; i < _steps; i++)
            { 
                currentPos += Directions[Random.Range(0, Directions.Count)];
                path.Add(currentPos);
            }

            return path;
        }
        
    }
}