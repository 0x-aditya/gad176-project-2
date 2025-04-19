using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester123 : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int stepSize;
    [SerializeField] private int complexity;
    private List<GameObject> instantiatedObjects;
    
    
    void Start()
    {
        instantiatedObjects = new List<GameObject>();
        MakeNewDungeon();
    }

    private void MakeNewDungeon()
    {
        var walk = RandomWalk.ComplexWalk(new Vector2Int(0,0), stepSize, new Vector2Int(0,0),complexity);
        foreach (var dir in walk)
        {
            instantiatedObjects.Add(Instantiate(prefab, new Vector3(dir.x, 0, dir.y), Quaternion.identity));
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var obj in instantiatedObjects)
            {
                Destroy(obj);
            }
            instantiatedObjects.Clear();
            MakeNewDungeon();
        }
    }


}
