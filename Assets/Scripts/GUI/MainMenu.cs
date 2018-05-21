using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Play(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    private void Update()
    {
        Time.timeScale = 1f;
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
