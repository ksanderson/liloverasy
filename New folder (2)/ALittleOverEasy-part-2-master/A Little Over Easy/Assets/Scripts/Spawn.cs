using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour
{
    public GameObject itemToSpawn;
    private bool spawnable;
    [SerializeField]
    private int spawnIntervalMin;
    [SerializeField]
    private int spawnIntervalMax;
    [SerializeField]
    private int maxConcurrent;
    private int currentSpawnInterval;
    private float roll;

    // Use this for initialization
    void Start()
    {
        spawnable = true;
        currentSpawnInterval = 0;
        roll = Random.value * (spawnIntervalMax - spawnIntervalMin) + spawnIntervalMin;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= maxConcurrent && currentSpawnInterval >= roll)
        {
            currentSpawnInterval = 0;
            SpawnObject();
            roll = Random.value * (spawnIntervalMax - spawnIntervalMin) + spawnIntervalMin;
        }
    }

    //<summary> Spawns an item from our itemsToSpawn list at a random point between spawnStartPoint and spawnEndPoint</summary>
    public void SpawnObject()
    {
        Transform selectedSpawn = this.transform;
        //Instantiate (itemsToSpawn [itemIndex], new Vector3 (x, y, z), Quaternion.identity);
        GameObject go = Instantiate(itemToSpawn, new Vector3(selectedSpawn.position.x, selectedSpawn.position.y, selectedSpawn.position.z), Quaternion.identity) as GameObject;
        go.transform.SetParent(this.transform);
    }

    void OnTriggerStay(Collider other)
    {
        if (spawnable && other.tag.Equals("Player"))
        {
            currentSpawnInterval++;
        }
    }
}

