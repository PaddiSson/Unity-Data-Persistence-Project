using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HighScoreUIHandler : MonoBehaviour
{
    //* VARS
     public TextMeshProUGUI textCongrats;
    public Text textScore;

    //* MAIN METHODS
    // Start is called before the first frame update
    void Start()
    {
        textCongrats.text = "New high score " + DataManager.Instance.highScorePlayerNickname + "!";
        textScore.text = DataManager.Instance.highScore.ToString();
    }

    // Go back to the menu
    public void PlayAgain()
    {
        // Load scene
        SceneManager.LoadScene(1);
    }
}
