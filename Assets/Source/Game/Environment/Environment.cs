using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public static Environment Instance { get; private set; }

    public Terrain GameMap { get; private set; }

    public List<GameObject> MyHarvestableObjects { get; private set; }
    public List<Spawner> MySpawners { get; private set; }
    public DeerSpawner MyDeerSpawner { get; private set; }
    public Queue<Item> DroppedItems { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Instance = this;

        MyHarvestableObjects = new List<GameObject>();
        MySpawners = new List<Spawner>();

        GameMap = GameObject.Find("Terrain").GetComponent<Terrain>();

        GameObject[] MySpawnerObjects = GameObject.FindGameObjectsWithTag("Spawners");
        foreach (GameObject spawner in MySpawnerObjects)
            MySpawners.Add(spawner.GetComponent<Spawner>());

        MyDeerSpawner = GameObject.Find("DeerSpawner").GetComponent<DeerSpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {       
        foreach (Spawner spawner in MySpawners)
            foreach (GameObject spawnedObject in spawner.MySpawnedObjects)
                MyHarvestableObjects.Add(spawnedObject);

        foreach (GameObject spawnedObject in MyDeerSpawner.MySpawnedObjects)
            MyHarvestableObjects.Add(spawnedObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
