using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public void SavePlayerPosData(float x, float y)
    {
        PlayerPrefs.SetFloat("playerPosX", x);
        PlayerPrefs.SetFloat("playerPosY", y);
        PlayerPrefs.Save();
    }

    public Vector2 RetrievePlayerPosData()
    {
        float playerPosY = PlayerPrefs.GetFloat("playerPosY");
        float playerPosX = PlayerPrefs.GetFloat("playerPosX");
        return new Vector2(playerPosX, playerPosY);
    }

    public void SaveEnemyExistData(string enemyName, bool exist)
    {
        if (exist)
        {
            PlayerPrefs.SetInt(enemyName, 1);
        }
        else
        {
            PlayerPrefs.SetInt(enemyName, 0);
        }

        PlayerPrefs.Save();
    }

    public bool RetrieveEnemyExistData(string enemyName)
    {
        if (PlayerPrefs.GetInt(enemyName) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
