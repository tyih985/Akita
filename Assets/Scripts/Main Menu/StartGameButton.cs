using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{

    public void DestroyPersistentData()
    {
        if (PersistentData.Instance != null)
        {
            PersistentData.DestroyInstance();
        }
    }
}
