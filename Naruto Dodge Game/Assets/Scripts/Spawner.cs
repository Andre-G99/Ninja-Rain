using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] hazards;

    private float timeBetweenSpawns;
    public float startTimeBetweenSpawns;
    public float minTimeBetweenSpawns;
    public float decrease;

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
      if (player != null)
      {
        if(timeBetweenSpawns <= 0)
        {
              //providing random spawnpoints and hazards
              Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
              GameObject randomHazard = hazards[Random.Range(0, hazards.Length)];


              //spawning hazards
              Instantiate(randomHazard, randomSpawnPoint.position,Quaternion.identity);

              //checking and changing difficulty
              if(startTimeBetweenSpawns > minTimeBetweenSpawns)
              {
                  startTimeBetweenSpawns -=decrease;
              }

              //resetting time between spawns so there doesnt spawn each frame
              timeBetweenSpawns = startTimeBetweenSpawns;

        }

        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
      }
    }
}
