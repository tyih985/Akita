using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private static PersistentData instance;

    public static PersistentData Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private Player player;
    private Enemy enemy;
    private readonly Dictionary<string, EnemyStatus> enemiesStatus = new();

    public Player GetPlayer() { return player; }

    public void SetPlayer(Player player) { this.player = player; }

    public Enemy GetEnemy() { return enemy; }

    public void SetEnemy(Enemy enemy) { this.enemy = enemy; }

    public (bool, EnemyStatus) TryGetEnemyStatus(string enemyID)
    {
        EnemyStatus status;
        return (enemiesStatus.TryGetValue(enemyID, out status), status);
    }

    public void SetEnemyStatus(string enemyID, EnemyStatus status) { enemiesStatus[enemyID] = status; }
    
    public void AddEnemyStatus(string enemyID, EnemyStatus status) { enemiesStatus.Add(enemyID, status); }

    public static void DestroyInstance() { Destroy(instance.gameObject); }
}
