using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    private Button quit_Button;
    private Button menu_Button;
    private Button retry_Button;

    public TextMeshProUGUI highscores_List;

    void Start()
    {
        highscores_List.text = "Best Scores<br>";

        foreach (var highscore in DataManager.dataManager.data.highScores)
        {
            highscores_List.text += "<br>" + highscore.PlayerName.ToUpper() + " : " + highscore.PlayerScore;
        }

    }

    public void quit_Game()
    {
        Debug.Log("Click Quit");

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void return_to_Menu()
    {
        Debug.Log("Click Menu");
        SceneManager.LoadScene(0);
    }
    public void retry_Game()
    {
        Debug.Log("Click Retry");
        SceneManager.LoadScene(1);

        Player.PlayerInstance.resetPlayerScore();
    }
}
