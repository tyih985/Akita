using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string nextSceneName;
    public float transitionDuration = 1.0f;
    public GameObject transitionScreen;

    public void LoadScene()
    {
        StartCoroutine(LoadSceneTransition());
    }

    private IEnumerator LoadSceneTransition()
    {
        transitionScreen.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(nextSceneName);
    }
}
