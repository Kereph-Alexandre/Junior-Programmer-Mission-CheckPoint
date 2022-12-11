using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance;

    private string m_playerName;

    public string getPlayerName()
    {
        return m_playerName;
    }

    public void setPlayerName(string playerName)
    {
        this.m_playerName = playerName;
    }

    void Awake()
    {
        if (PlayerInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            PlayerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
