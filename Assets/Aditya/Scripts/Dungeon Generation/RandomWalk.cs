using System.Collections.Generic;
using UnityEngine;

namespace Aditya.Scripts.Dungeon_Generation
{
    public class RandomWalk
    {
        protected readonly int Steps;
        protected static readonly List<Vector2Int> Directions = new List<Vector2Int>
        {
            new(0, 1), // Up
            new(1, 0), // Right
            new(0, -1), // Down
            new(-1, 0) // Left
        }; // directions for the random walk

        public RandomWalk(int steps)
        {
            Steps = steps;
        }

        public virtual HashSet<Vector2Int> GenerateWalk(Vector2Int startPosition)
        {
            // create a set to store the path
            HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
            var currentPos = startPosition; // set current position to start position
            for (int i = 0; i < Steps; i++)
            { 
                // adding random position to the path
                currentPos += Directions[Random.Range(0, Directions.Count)];
                // adds the position to the path
                path.Add(currentPos);
            }

            return path;
        }
        
    }
}