using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;

public class MainManager : MonoBehaviour
{
    // Needed to create the stage 
    public Brick BrickPrefab;
    public Rigidbody Ball;
    public int LineCount = 6;

    // Needed to display current score, highscore and gameover
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    // Game state related attributes
    private bool m_Started = false;
    private bool m_GameOver = false;

    // Storing Highscore playerName and score; 
    public string highScorePlayerNameDisplay;
    public int highScorePlayerScoreDisplay;

    // trigger saveHighScoreMethod
    public bool needToSave;

    /* AWAKE
    Loads stored Highscore, 
    Refreshes the highcore display text,  
    disables the SaveHighScore method call.   */
    void Awake()
    {
        LoadHighScore();

        needToSave = false;

        DisplayHighScore();
    }


    // Start is called before the first frame update
    /* Instantiates the target blocks, with their color and point value. */
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    /* UPDATE called every frame
    starts the game on space input, 
    Sets the direction and force of the ball's movement, 
    at gameOver, if space is pressed : reset the Player's current score and reloads the scene */
    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.PlayerInstance.resetPlayerScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    /* Add points to the player's score, 
    updates the HUD, 
    triggers Highscore save if current score is higher. */
    void AddPoint(int point)
    {
        Player.PlayerInstance.addPlayerPoint(point);
        ScoreText.text = $"Score for {Player.PlayerInstance.getPlayerName()}: {Player.PlayerInstance.getPlayerScore()}";

        if (Player.PlayerInstance.getPlayerScore() > highScorePlayerScoreDisplay)
        {
            highScorePlayerNameDisplay = Player.PlayerInstance.getPlayerName();
            highScorePlayerScoreDisplay = Player.PlayerInstance.getPlayerScore();

            DisplayHighScore();

            needToSave = true;
        }
    }

    /* Displays gameOver text, 
    Saves Highscore if needed. */
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (needToSave)
        {
            SaveHighScore();
        }
    }

    [System.Serializable]
    class GameData
    {
        public string highScorePlayerName;
        public int highScorePlayerScore;
    }

    // Stores Name and score of the player in Json file
    public void SaveHighScore()
    {
        GameData gameData = new GameData();

        gameData.highScorePlayerName = Player.PlayerInstance.getPlayerName();
        gameData.highScorePlayerScore = Player.PlayerInstance.getPlayerScore();

        string json = JsonUtility.ToJson(gameData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load name and socre of the highscore from json file
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            highScorePlayerNameDisplay = data.highScorePlayerName;
            highScorePlayerScoreDisplay = data.highScorePlayerScore;

        }
    }

    // Refreshes HUD's Highscore
    public void DisplayHighScore()
    {
        HighScoreText.text = $"Best Score : {this.highScorePlayerNameDisplay}: {this.highScorePlayerScoreDisplay}";
    }
}
