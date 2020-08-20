using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerSpawner : MonoBehaviour
{
    public GameObject MySpawnableObject;
    public List<GameObject> MySpawnedObjects { get; private set; }

    public int MaxObjectsNumber { get; private set; }

    public float AreaDeltaX { get; private set; }
    public float AreaDeltaZ { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        MySpawnedObjects = new List<GameObject>();

        AreaDeltaX = 250f;
        AreaDeltaZ = 250f;

        MaxObjectsNumber = 20;

        for (int objectIndex = 1; objectIndex <= MaxObjectsNumber; objectIndex++)
        {
            MySpawnedObjects.Add(Instantiate(MySpawnableObject, Vector3.zero, Quaternion.identity));
            MySpawnedObjects[MySpawnedObjects.Count - 1].name = MySpawnableObject.name + "Instance" + objectIndex;
            MySpawnedObjects[MySpawnedObjects.Count - 1].transform.parent = gameObject.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawnedObject in MySpawnedObjects)
            PositionObject(spawnedObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject spawnedObject in MySpawnedObjects)
            if (spawnedObject.transform.position == Vector3.zero)
                PositionObject(spawnedObject);
    }

    void PositionObject(GameObject go)
    {
        float newPositionX = Random.Range(Game.Instance.MyPlayer.transform.position.x - AreaDeltaX, Game.Instance.MyPlayer.transform.position.x + AreaDeltaX);
        float newPositionZ = Random.Range(Game.Instance.MyPlayer.transform.position.z - AreaDeltaZ, Game.Instance.MyPlayer.transform.position.z + AreaDeltaZ);

        bool isPositionFinal = false;

        if (Environment.Instance.MyHarvestableObjects.Count != 0)
        {
            while (isPositionFinal == false)
            {
                isPositionFinal = true;

                foreach (GameObject spawnedObject in Environment.Instance.MyHarvestableObjects)
                {
                    if (spawnedObject.transform.position.x == newPositionX && spawnedObject.transform.position.z == newPositionZ)
                    {
                        isPositionFinal = false;
                    }
                }

                if (isPositionFinal == false)
                {
                    newPositionX = Random.Range(Game.Instance.MyPlayer.transform.position.x - AreaDeltaX, Game.Instance.MyPlayer.transform.position.x + AreaDeltaX);
                    newPositionZ = Random.Range(Game.Instance.MyPlayer.transform.position.z - AreaDeltaZ, Game.Instance.MyPlayer.transform.position.z + AreaDeltaZ);
                }
            }
        }

        float newPositionY = Environment.Instance.GameMap.SampleHeight(new Vector3(newPositionX, 0, newPositionZ));

        Vector3 newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
        go.GetComponent<NavMeshAgent>().Warp(newPosition);
    }

    public void SpawnAllObjects()
    {
        foreach (GameObject spawnedObject in MySpawnedObjects)
            PositionObject(spawnedObject);
    }
}
