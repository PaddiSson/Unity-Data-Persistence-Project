using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class DataManager : MonoBehaviour
{
    //* VARS
    public static DataManager Instance;
    public string playerNickname;
    public string highScorePlayerNickname;
    public int highScore;
    
    // Awake - Called as soon as the object is created
    private void Awake()
    {
        // Check whether or not Instance is null
        // This is a singleton = ensure that only a single instance of the DataManager can ever exist, so it acts as a central point of access
        if (Instance != null)
        {
            // The extra DataManager is destroyed and the script exits there
            Destroy(gameObject);
            return;
        }

        // Store "this" in the class member Instance - the current instance of DataManager.
        // Can now call DataManager.Instance from any other script and get a link to that specific Instance of it
        // Dont need to have a reference to it, like assign GameObjects to script properties in Inspector
        Instance = this;
        // Marks the DataManager GameObject attached to this script not to be destroyed when the scene changes
        DontDestroyOnLoad(gameObject);

        //- Load the high score as persistent data from JSON file
        LoadHighScore();
        Debug.Log(Application.persistentDataPath);
    }


    // Simple class which contains the playerNickname that the user selects
    // Why are you creating a class and not giving the DataManager instance directly to the JsonUtility? 
    // Well, most of the time you won’t save everything inside your classes. It’s good practice and more efficient to use a small class that only contains the specific data that you want to save.
    [System.Serializable] // Line required for JsonUtility
    class SaveData
    {
        public string highScorePlayerNickname;
        public int highScore;
    }

    //- Save the high score as persistent data in JSON file
    public void SaveHighScore()
    {
        // Create a new instance of the save data
        SaveData data = new SaveData();
        Debug.Log("SaveData : " + playerNickname + " - " + highScore);
        // Fill class member with the playerNickname variable
        data.highScorePlayerNickname = playerNickname;
        data.highScore = highScore;

        // Transform that instance to JSON
        string json = JsonUtility.ToJson(data);
        // Use method File.WriteAllText to write a string to a file
        // persistentDataPath INFO https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // Need Namespace : using System.IO;
    }

    //- Load the high score as persistent data from JSON file
    public void LoadHighScore()
    {
        // Get path and file of persistent data
        string path = Application.persistentDataPath + "/savefile.json";

        // If file exists
        if (File.Exists(path))
        {
            // Read the file content with File.ReadAllText
            string json = File.ReadAllText(path);
            // Transform it back into a SaveData instance
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Set the playerNickname/highScore to the playerNickname/highScore saved in that SaveData
            highScorePlayerNickname = data.highScorePlayerNickname;
            highScore = data.highScore;
        }
    }
}
