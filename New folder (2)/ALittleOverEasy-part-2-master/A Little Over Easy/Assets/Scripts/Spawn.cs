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

    private float baseAcceleration;
    private float baseSpeed;
    private float baseAngular;

    private float acceleration;
    private float speed;
    private float angular;

    // Use this for initialization
    void Start()
    {
        spawnable = true;
        currentSpawnInterval = 0;
        roll = Random.value * (spawnIntervalMax - spawnIntervalMin) + spawnIntervalMin;

        acceleration = baseAcceleration = itemToSpawn.GetComponent<NavMeshAgent>().acceleration;
        speed =        baseSpeed =        itemToSpawn.GetComponent<NavMeshAgent>().speed;
        angular =      baseAngular =      itemToSpawn.GetComponent<NavMeshAgent>().angularSpeed;
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
        GameObject go = Instantiate(itemToSpawn, new Vector3(selectedSpawn.position.x, selectedSpawn.position.y, selectedSpawn.position.z), Quaternion.identity) as GameObject;
        go.transform.SetParent(this.transform);
        go.GetComponent<NavMeshAgent>().speed = speed;
        speed += baseSpeed / 2.0f;
        go.GetComponent<NavMeshAgent>().acceleration = acceleration;
        acceleration += baseAcceleration / 2.0f;
        go.GetComponent<NavMeshAgent>().angularSpeed = angular;
        angular += baseAngular / 2.0f;
    }

    void OnTriggerStay(Collider other)
    {
        if (spawnable && other.tag.Equals("Player"))
        {
            currentSpawnInterval++;
        }
    }
}

