using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject hpTextObj;
    public GameObject spTextObj;
    public GameObject pauseMenu;
    public GameObject campfireMenu;
    public GameObject blackScreen;
    public KeyCode pauseKey = KeyCode.Escape;

    private Player player;
    private TextMeshProUGUI hpText;
    private TextMeshProUGUI spText;
    private Animator transitionAnim;
    private bool pauseMenuToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<PlayerController>().player;
        hpText = hpTextObj.GetComponent<TextMeshProUGUI>();
        spText = spTextObj.GetComponent<TextMeshProUGUI>();
        transitionAnim = blackScreen.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP: " + player.character.hp + "/" + player.character.hp;
        spText.text = "SP: " + player.character.hp + "/" + player.character.hp;

        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        Time.timeScale = pauseMenuToggled ? 1 : 0;
        pauseMenuToggled = !pauseMenuToggled;
        pauseMenu.SetActive(pauseMenuToggled);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void ToggleCampfireMenu()
    {
        StartCoroutine(TransitionFade(() => {
            campfireMenu.SetActive(!campfireMenu.activeInHierarchy);
        }));
    }

    public void Rest()
    {
        player.characterList.ForEach(c =>
        {
            c.hp = c.maxhp;
            c.sp = c.maxsp;
        });

        StartCoroutine(TransitionFade(() =>
        {
            campfireMenu.SetActive(!campfireMenu.activeInHierarchy);
            playerObj.GetComponent<PlayerController>().TogglePaused();
        }));
    }

    public IEnumerator TransitionFade(Action afterTransition)
    {
        transitionAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(2);
        afterTransition();
        transitionAnim.SetTrigger("fadeIn");
    }
}
