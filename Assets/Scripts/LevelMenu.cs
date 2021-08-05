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

    public TextMeshProUGUI levelText;
    public GameObject PauseMenuPanal;
    public GameObject PauseButton;
    public GameObject UndoButton;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        audioManager = FindObjectOfType<AudioManager>();
        if (int.TryParse(sceneName.Substring(5), out currentLevel))
        {
            levelText.text = "Level " + currentLevel.ToString();
        }
        else
        {
            Debug.Log("ERROR: Something went wrong, cannot parse int current level");
        }
    }

    public void PauseGame()
    {
        audioManager.Play("btnClickSound");
        PauseMenuPanal.SetActive(true);
        PauseButton.SetActive(false);
        UndoButton.SetActive(false);
    }
    public void ResumeGame()
    {
        audioManager.Play("btnClickSound");
        PauseMenuPanal.SetActive(false);
        PauseButton.SetActive(true);
        UndoButton.SetActive(true);
    }

    public void UndoMove()
    {
        audioManager.Play("btnClickSound");
        GameObject.Find("Player").GetComponent<Player>().UndoMove();
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
