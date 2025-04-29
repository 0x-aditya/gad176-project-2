using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Aditya.Scripts.Dungeon_Generation
{
    public class IterativeRandomWalk : RandomWalk
    {
        private readonly int _complexity;
        public IterativeRandomWalk(int steps, int complexity) : base(steps)
        {
            _complexity = complexity;
        }

        public override HashSet<Vector2Int> GenerateWalk(Vector2Int startPosition)
        {
            // create a set to store the path
            HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
            
            //complexity is the number of iterations the random walk will take
            for (int i = 0; i < _complexity; i++)
            {
                var currentPos = startPosition; // reset current position to start position
                for (int j = 0; j < Steps; j++)
                { 
                    // adding random position to the path
                    currentPos += Directions[Random.Range(0, Directions.Count)];
                    path.Add(currentPos);
                }
            }

            return path;
        }
    }
}