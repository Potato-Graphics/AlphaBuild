using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnEnemy : IComparable<RespawnEnemy>
{
    public GameObject enemy;
    public Vector3 spawnPoint;
    public int enemyID;


    public RespawnEnemy(int ID, Vector3 spawnLocation, GameObject enemyPrefab)
    {
        enemyID = ID;
        spawnPoint = spawnLocation;
        enemy = enemyPrefab;
    }
    public int CompareTo(global::RespawnEnemy other)
    {
            return enemyID - other.enemyID;
    }
}
