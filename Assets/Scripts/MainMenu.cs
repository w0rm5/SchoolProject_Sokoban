using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    public TextMeshProUGUI TitleText;
    public GameObject MainMenuObject;
    public GameObject LevelSelectPanel;
    public GameObject BackButton;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("mainBgMusic");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && LevelSelectPanel.activeSelf)
        {
            BackMainMenu();
        }
    }

    public void StartGame()
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene("Level1");
    }

    public void ShowLevelSelectMenu()
    {
        audioManager.Play("btnClickSound");
        TitleText.text = "SELECT LEVEL";
        MainMenuObject.SetActive(false);
        LevelSelectPanel.SetActive(true);
        BackButton.SetActive(true);
    }

    public void SelectLevel(string level)
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene("Level" + level);
    }

    public void BackMainMenuClick()
    {
        audioManager.Play("btnClickSound");
        BackMainMenu();
    }

    private void BackMainMenu()
    {
        TitleText.text = "SOKOBAN GAME";
        LevelSelectPanel.SetActive(false);
        BackButton.SetActive(false);
        MainMenuObject.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.Play("btnClickSound");
        Application.Quit();
    }
}
