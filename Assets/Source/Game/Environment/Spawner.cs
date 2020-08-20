using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject MySpawnableObject;
    public List<GameObject> MySpawnedObjects { get; private set; }

    public int MaxObjectsNumber { get; private set; }

    public float AreaStartX { get; private set; }
    public float AreaStartZ { get; private set; }
    public float AreaEndX { get; private set; }
    public float AreaEndZ { get; private set; }

    public int ObjectIndex { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        MySpawnedObjects = new List<GameObject>();
        MaxObjectsNumber = 2000;

        for (int objectIndex = 1; objectIndex <= MaxObjectsNumber; objectIndex++)
        {
            MySpawnedObjects.Add(Instantiate(MySpawnableObject, Vector3.zero, Quaternion.identity));
            MySpawnedObjects[MySpawnedObjects.Count - 1].name = MySpawnableObject.name + "Instance" + objectIndex;
            MySpawnedObjects[MySpawnedObjects.Count - 1].transform.parent = gameObject.transform;
        }

        ObjectIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        AreaStartX = Environment.Instance.GameMap.transform.position.x;
        AreaStartZ = Environment.Instance.GameMap.transform.position.z;
        AreaEndX = AreaStartX + Environment.Instance.GameMap.terrainData.size.x;
        AreaEndZ = AreaStartZ + Environment.Instance.GameMap.terrainData.size.z;

        foreach (GameObject spawnedObject in MySpawnedObjects)
            PositionObject(spawnedObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (MySpawnedObjects[ObjectIndex].transform.position == Vector3.zero)
            PositionObject(MySpawnedObjects[ObjectIndex]);
        ObjectIndex++;

        if (ObjectIndex == MySpawnedObjects.Count)
            ObjectIndex = 0;
    }

    void PositionObject(GameObject go)
    {
        float newPositionX = Random.Range(AreaStartX, AreaEndX);
        float newPositionZ = Random.Range(AreaStartZ, AreaEndZ);

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
                    newPositionX = Random.Range(AreaStartX, AreaEndX);
                    newPositionZ = Random.Range(AreaStartZ, AreaEndZ);
                }
            }
        }
        float newPositionY = Environment.Instance.GameMap.SampleHeight(new Vector3(newPositionX, 0, newPositionZ));
        if (MySpawnableObject.name.Contains("Tree"))
            newPositionY += 3.25f;
        else
            newPositionY += MySpawnableObject.transform.localScale.y / 2;

        Vector3 newPosition = new Vector3(newPositionX, newPositionY, newPositionZ);
        go.transform.position = newPosition;
    }

    public void SpawnAllObjects()
    {
        foreach (GameObject spawnedObject in MySpawnedObjects)
            PositionObject(spawnedObject);
    }
}
