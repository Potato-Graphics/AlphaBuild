using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnEnemy : IComparable<RespawnEnemy>
{
    public GameObject enemy;
    public Vector3 spawnPoint;
    public int enemyID;
    public int MAX_HEALTH;


    public RespawnEnemy(int ID, Vector3 spawnLocation, GameObject enemyPrefab, int MAX_HEALTH)
    {
        enemyID = ID;
        spawnPoint = spawnLocation;
        enemy = enemyPrefab;
        this.MAX_HEALTH = MAX_HEALTH;
    }
    public int CompareTo(global::RespawnEnemy other)
    {
            return enemyID - other.enemyID;
    }
}
