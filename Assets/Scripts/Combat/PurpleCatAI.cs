using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleCatAI : EnemyAI
{
    public PurpleCatAI()
    {
        maxhp = 50;
        hp = maxhp;
    }

    private CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
    }

    public override void PerformAttack()
    {
        combatManager.GetPlayer().character.hp -= 10;
    }
}
