using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject slashObj;

    private Player player;
    private EnemyAI enemy;
    private Enemy currentEnemy;
    private PersistentData persistentData;
    private CombatUIManager uiManager;
    private Animator slashAnimator;
    private List<Characters> turnOrder = new() { Characters.Jin, Characters.Byron, Characters.Erik, Characters.Ina };
    private int turnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<CombatUIManager>();

        persistentData = PersistentData.Instance;

        currentEnemy = persistentData.GetEnemy();
        SpriteRenderer spriteRenderer = currentEnemy.prefab.GetComponent<SpriteRenderer>();
        EnemyAI enemyAI = currentEnemy.prefab.GetComponent<EnemyAI>();

        Image enemyImage = enemyObj.GetComponent<Image>();
        enemyImage.sprite = spriteRenderer.sprite;
        enemyImage.SetNativeSize();

        enemyObj.AddComponent(enemyAI.GetType());
        enemy = enemyObj.GetComponent<EnemyAI>();
        enemyObj.SetActive(true);

        player = persistentData.GetPlayer();
        player.character = player.characterList[0];

        slashAnimator = slashObj.GetComponent<Animator>();
    }

    private void TriggerSlash()
    {
        slashAnimator.SetTrigger("slashTrigger");
    }

    private bool CheckHP()
    {
        if (enemy.hp <= 0)
        {
            persistentData.SetEnemyStatus(currentEnemy.id, EnemyStatus.Dead);
            uiManager.ShowVictory();
            return false;
        }
        else if (player.character.hp <= 0)
        {
            Flee();
            return false;
        }
        return true;
    }

    public void Flee()
    {
        persistentData.SetEnemyStatus(currentEnemy.id, EnemyStatus.Alive);
        uiManager.ShowDefeat();
    }

    public void Attack(int damage)
    {
        TriggerSlash();
        enemy.hp -= damage;
        if (CheckHP())
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        if (turnCount < 3)
        {
            print(turnCount);
            turnCount++;
            uiManager.SwitchCharacter(turnOrder[turnCount-1], turnOrder[turnCount]);
            SwitchCharacter(turnCount);
        }
        else
        {
            uiManager.DisableActions();
            enemy.PerformAttack();
            if (CheckHP())
            {
                StartTurn();
            }
        }
    }

    private void SwitchCharacter(int index)
    {
        player.character = player.characterList[(int)turnOrder[index]];
    }

    private void StartTurn()
    {
        uiManager.EnableActions();
        uiManager.SwitchCharacter(turnOrder[turnCount], turnOrder[0]);
        turnCount = 0;
        SwitchCharacter(turnCount);
    }

    public CombatUIManager GetUIManager() { return uiManager; }

    public EnemyAI GetEnemy() { return enemy; }

    public Player GetPlayer() { return player; }
}
