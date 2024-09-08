using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : EnemyAI
{
    public SlimeAI()
    {
        maxhp = 70;
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
