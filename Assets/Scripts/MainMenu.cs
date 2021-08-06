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
    public GameObject QuitConfirmPanel;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("mainBgMusic");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && LevelSelectPanel.activeSelf)
        {
            MainMenuSetting(true);
        }
    }

    public void StartGame()
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene("Level1");
    }

    private void MainMenuSetting(bool showMainMenu)
    {
        TitleText.text = showMainMenu ? "SOKOBAN GAME": "SELECT LEVEL";
        LevelSelectPanel.SetActive(!showMainMenu);
        BackButton.SetActive(!showMainMenu);
        MainMenuObject.SetActive(showMainMenu);
    }

    public void ShowMainMenu(bool showMainMenu)
    {
        audioManager.Play("btnClickSound");
        MainMenuSetting(showMainMenu);
    }

    public void SelectLevel(string level)
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene("Level" + level);
    }

    public void QuitConfirm(bool show)
    {
        audioManager.Play("btnClickSound");
        QuitConfirmPanel.SetActive(show);
    }

    public void QuitGame()
    {
        audioManager.Play("btnClickSound");
        Application.Quit();
    }
}
