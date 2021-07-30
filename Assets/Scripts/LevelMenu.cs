using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    //set the number of levels here
    private readonly int levelNum = 1;
    private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        if(int.TryParse(sceneName.Substring(5), out int currentLevel))
        {
            //check if current level is not the last level
            if(currentLevel < levelNum)
            {
                SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
            }
            //load level 1 if it's the last level
            else
            {
                SceneManager.LoadScene("Level1");
            }
        }
        else
        {
            Debug.Log("Cannot parse int current level");
        }
    }
}
