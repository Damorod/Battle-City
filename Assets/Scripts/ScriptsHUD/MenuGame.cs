using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    private void Start()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGameAgain()
    {
        SceneManager.LoadScene("Menu");
    }
}
