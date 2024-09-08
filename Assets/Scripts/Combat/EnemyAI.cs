using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int hp;
    public int maxhp;

    public virtual void PerformAttack() { }
}
