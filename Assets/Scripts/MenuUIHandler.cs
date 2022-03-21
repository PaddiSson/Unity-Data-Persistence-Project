using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{
    //* VARS
    public TMP_InputField inputFieldPlayerNickname;
    public Text highScore;

    public void Start()
    {
        if (DataManager.Instance.highScore == 0)
        {
            highScore.text = "No high score yet!";
        }
        else 
        {
            highScore.text = "High score : " + DataManager.Instance.highScorePlayerNickname + " - " + DataManager.Instance.highScore;
        }
    }
    
    // Start a new game
    public void StartNewGame()
    {
        Debug.Log("Start new game - Current player : " + inputFieldPlayerNickname.text);
        // Save the nickname as persistent data
        DataManager.Instance.playerNickname = inputFieldPlayerNickname.text;
        // Load scene
        SceneManager.LoadScene(1);
    }

    // Exit the application
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode(); // Exit mode inside the Editor 
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }
}
