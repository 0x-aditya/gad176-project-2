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
            HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
            for (int i = 0; i < _complexity; i++)
            {
                var currentPos = startPosition;
                for (int j = 0; j < _steps; j++)
                { 
                    currentPos += Directions[Random.Range(0, Directions.Count)];
                    path.Add(currentPos);
                }
            }

            return path;
        }
    }
}