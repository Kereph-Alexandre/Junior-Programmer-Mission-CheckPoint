using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance;

    private string m_playerName;
    private int m_playerScore;

    public string getPlayerName()
    {
        return m_playerName;
    }

    public void setPlayerName(string playerName)
    {
        this.m_playerName = playerName;
    }

    public int getPlayerScore()
    {
        return m_playerScore;
    }

    public void addPlayerPoint(int points)
    {
        this.m_playerScore += points;
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

