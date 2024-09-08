using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public EnemyStatus status = EnemyStatus.Alive;
    public string id = "";
    public string name = "";
    public GameObject prefab = null;
    public float posX = 0;
    public float posY = 0;
}

[System.Serializable]
public enum EnemyStatus
{
    Alive,
    Dead
}
