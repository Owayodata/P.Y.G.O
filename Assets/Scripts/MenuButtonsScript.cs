using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayBTN()
    {
        PlayerPrefs.DeleteKey("score");
        SceneManager.LoadScene("Level1");
    }
    public void QuitBTN()
    {
        Application.Quit();
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("score", Ball.scoreReset);
    }

    public void Highscore()
    {
        SceneManager.LoadScene("Highscore");
    }
}
