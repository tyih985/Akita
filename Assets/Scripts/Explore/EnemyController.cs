using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy thisEnemy;

    private PersistentData persistentData;

    void Start()
    {
        persistentData = PersistentData.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        persistentData.SetEnemy(thisEnemy);
    }
}
