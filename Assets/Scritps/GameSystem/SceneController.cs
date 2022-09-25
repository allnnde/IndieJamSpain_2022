using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    private void Start()
    {
        // Used to exit from Core screen
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlayMenuMusic();
    }

    public void LoadGameplayscene()
    {
        SceneManager.LoadScene("Map");
        AudioManager.Instance.StopMenuMusic();

    }

}
