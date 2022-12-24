using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public string highScorePlayerNameDisplay;
    public int highScorePlayerScoreDisplay;

    void Awake()
    {
        LoadHighScore();
        DisplayHighScore();
    }


    // Start is called before the first frame update
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        Player.PlayerInstance.addPlayerPoint(point);
        ScoreText.text = $"Score for {Player.PlayerInstance.getPlayerName()}: {Player.PlayerInstance.getPlayerScore()}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        Debug.Log("Sauvegarde de HighScore");
        SaveHighScore();
    }

    [System.Serializable]
    class GameData
    {
        public string highScorePlayerName;
        public int highScorePlayerScore;
    }

    public void SaveHighScore()
    {
        GameData gameData = new GameData();

        gameData.highScorePlayerName = Player.PlayerInstance.getPlayerName();
        gameData.highScorePlayerScore = Player.PlayerInstance.getPlayerScore();

        string json = JsonUtility.ToJson(gameData);

        Debug.Log(json);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

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


    public void DisplayHighScore()
    {
        HighScoreText.text = $"Best Score : {this.highScorePlayerNameDisplay}: {this.highScorePlayerScoreDisplay}";
    }
}
