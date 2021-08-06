using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    //set the number of levels here
    private readonly int levelNum = 15;
    private AudioManager audioManager;
    private string sceneName;
    private int currentLevel;
    private Player player;

    public TextMeshProUGUI levelText;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        sceneName = SceneManager.GetActiveScene().name;
        audioManager = FindObjectOfType<AudioManager>();
        if (int.TryParse(sceneName.Substring(5), out currentLevel))
        {
            levelText.text = "Level " + currentLevel.ToString();
        }
    }

    public void PauseGame()
    {
        audioManager.Play("btnClickSound");
        player.PauseGame(true);
    }
    public void ResumeGame()
    {
        audioManager.Play("btnClickSound");
        player.PauseGame(false);
    }

    public void UndoMove()
    {
        audioManager.Play("btnClickSound");
        player.UndoMove();
    }

    public void RestartGame()
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene(sceneName);
    }

    public void GoBackHome()
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        audioManager.Play("btnClickSound");
        //check if current level is not the last level
        if (currentLevel < levelNum)
        {
            SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
        }
        //load level 1 if it's the last level
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
