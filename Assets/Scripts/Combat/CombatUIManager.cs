using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour
{
    public GameObject hpTextObj;
    public GameObject spTextObj;
    public GameObject enemyHpTextObj;

    public GameObject pauseMenu;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public GameObject playerActions;
    public KeyCode pauseKey = KeyCode.Escape;

    public GameObject characterArtObj;
    public List<GameObject> barsList = new();
    public List<Sprite> characterArt = new();
    public List<Sprite> miniBars = new();
    public List<Sprite> bigBars = new();

    private CombatManager combatManager;
    private Player player;
    private EnemyAI enemy;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI spText;
    private TextMeshProUGUI enemyHpText;
    private bool pauseMenuToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GetComponent<CombatManager>();
        player = combatManager.GetPlayer();
        enemy = combatManager.GetEnemy();
        characterArtObj.GetComponent<Image>().sprite = characterArt[(int)player.character.character];
        SetAsBigBar(barsList[(int)player.character.character], bigBars[(int)player.character.character]);

        //hpText = hpTextObj.GetComponent<TextMeshProUGUI>();
        //spText = spTextObj.GetComponent<TextMeshProUGUI>();
        enemyHpText = enemyHpTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //hpText.text = "HP: " + player.character.hp + "/" + player.character.hp;
        //spText.text = "HP: " + player.character.hp + "/" + player.character.hp;
        enemyHpText.text = "HP: " + enemy.hp + "/" + enemy.maxhp;

        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenuToggled = !pauseMenuToggled;
        pauseMenu.SetActive(pauseMenuToggled);
    }

    public void DisableActions()
    {
        foreach (Button button in playerActions.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }
    }
    public void EnableActions()
    {
        foreach (Button button in playerActions.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
    }

    public void ShowVictory()
    {
        victoryScreen.SetActive(true);
    }

    public void ShowDefeat()
    {
        defeatScreen.SetActive(true);
    }

    public void SwitchCharacter(Characters previousCharacter, Characters character)
    {
        characterArtObj.GetComponent<Image>().sprite = characterArt[(int)character];
        SetAsBigBar(barsList[(int)character], bigBars[(int)character]);
        SetAsSmallBar(barsList[(int)previousCharacter], miniBars[(int)previousCharacter]);
    }

    private void SetAsBigBar(GameObject bar, Sprite bigSprite)
    {
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(bar.GetComponent<RectTransform>().sizeDelta.x * 1.5f, bar.GetComponent<RectTransform>().sizeDelta.y * 1.5f);
        bar.GetComponent<Image>().sprite = bigSprite;
        print("Setting " + bar.name + " as the big bar");
    }

    private void SetAsSmallBar(GameObject bar, Sprite smallSprite)
    {
        bar.GetComponent<RectTransform>().sizeDelta = new Vector2(bar.GetComponent<RectTransform>().sizeDelta.x / 1.5f, bar.GetComponent<RectTransform>().sizeDelta.y / 1.5f);
        bar.GetComponent<Image>().sprite = smallSprite;
        print("Setting " + bar.name + " as a small bar");
    }
}
