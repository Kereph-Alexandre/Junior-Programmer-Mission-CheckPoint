using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI playerNameTextInput;

    public void StartNew()
    {
        UsePlayerName();
        LoadMainScene();
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    private void UsePlayerName()
    {
        Player.PlayerInstance.setPlayerName(playerNameTextInput.text);
    }
}
