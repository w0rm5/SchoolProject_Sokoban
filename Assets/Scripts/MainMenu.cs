using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("mainBgMusic");
    }

    public void StartGame()
    {
        audioManager.Play("btnClickSound");
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        audioManager.Play("btnClickSound");
        Application.Quit();
    }
}
