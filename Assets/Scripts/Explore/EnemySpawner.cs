using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    private PersistentData persistentData;

    public List<Enemy> enemies = new();

    void Start()
    {
        persistentData = PersistentData.Instance;

        foreach (Enemy enemy in enemies)
        {
            (bool, EnemyStatus) status = persistentData.TryGetEnemyStatus(enemy.id);

            if (!status.Item1) // If the enemy has yet to be recorded, record it
            {
                persistentData.AddEnemyStatus(enemy.id, EnemyStatus.Alive);
            }
            else if (status.Item2 == EnemyStatus.Dead) // If the enemy is dead, don't spawn it
            {
                continue;
            }

            GameObject enemyObject = Instantiate(enemy.prefab, new Vector2(enemy.posX, enemy.posY), Quaternion.identity);
            enemyObject.GetComponent<EnemyController>().thisEnemy = enemy;
        }
    }
}