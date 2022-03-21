using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    public Text playerNicknameText; // Player nickname UI text field
    public Text highScoreText; // High score UI text field (score and nickname)

    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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

        Debug.Log("<color=red>START MainManager.cs</color>");
        Debug.Log(" -> Current player : " + DataManager.Instance.playerNickname);
        Debug.Log(" -> High score : " + DataManager.Instance.highScorePlayerNickname + DataManager.Instance.highScore);
        // Display the nickname as persistent data from DataManager Instance
        playerNicknameText.text = "Current player : " + DataManager.Instance.playerNickname;
        // Display the high score as persistent data from DataManager Instance
        if (DataManager.Instance.highScore > 0)
        {
            highScoreText.text = "High score : " + DataManager.Instance.highScorePlayerNickname + " - " + DataManager.Instance.highScore;
        }
        else 
        {
            highScoreText.text = "No high score yet!";
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
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Update the high score after a game over
        UpdateHighScore();
    }

    // Go back to the menu
    public void GoToMenu()
    {
        // Load scene
        SceneManager.LoadScene(0);
    }

    // Update the high score after a game over
    public void UpdateHighScore()
    {
        // If points > high score saved
        if (m_Points > DataManager.Instance.highScore)
        {
            Debug.Log("<b>New high score detected</b> : " + m_Points);
            // Display text info
            highScoreText.text = "High score : " + DataManager.Instance.playerNickname + " - " + m_Points;
            // Save highScore and highScorePlayerNickname as persistent data
            DataManager.Instance.highScore = m_Points;
            DataManager.Instance.highScorePlayerNickname = DataManager.Instance.playerNickname;
            //- Save the high score as persistent data in JSON file
            DataManager.Instance.SaveHighScore(); 

            // Launch high score scene
            SceneManager.LoadScene(2);
        }

    }
}
