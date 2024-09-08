using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PersistentData.Instance != null)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
