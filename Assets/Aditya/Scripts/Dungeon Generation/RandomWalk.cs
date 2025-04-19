using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk
{
    public static List<Vector2Int> directions = new List<Vector2Int>
    {
        new(0, 1), // Up
        new(1, 0), // Right
        new(0, -1), // Down
        new(-1, 0) // Left
    };
    
    public static HashSet<Vector2Int> GenerateWalk(Vector2Int start, int steps, Vector2Int startPosition)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
        var currentPos = startPosition;
        for (int i = 0; i < steps; i++)
        { 
            currentPos += directions[Random.Range(0, directions.Count)];
            path.Add(currentPos);
        }

        return path;
    }
    public static HashSet<Vector2Int> ComplexWalk(Vector2Int start, int steps, Vector2Int startPosition, int complexity)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
        
        for (int i = 0; i < complexity; i++)
        {
            var newPath = GenerateWalk(start, steps, startPosition);
            path.UnionWith(newPath);
        }
        
        return path;
    }
    private static HashSet<Vector2Int> Walker(Vector2Int start, int steps, Vector2Int startPosition)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int> { startPosition };
        var currentPos = startPosition;
        for (int i = 0; i < steps; i++)
        { 
            currentPos += directions[Random.Range(0, directions.Count)];
            path.Add(currentPos);
        }
        
        return path;
    }
}