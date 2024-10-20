using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Testing : MonoBehaviour
{
    [SerializeField] public HighscoreTable highscoreTable;
    [SerializeField] public GameObject coverBTN;

    private void Start()
    {
        transform.Find("submitScoreBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            UI_Blocker.Show_Static();
            coverBTN.SetActive(true);
            int currentScore = PlayerPrefs.GetInt("score", 0);

            UIinputWindow.Show_Static("Score", currentScore, () =>
            {
                // Clicked cancel
                UI_Blocker.Hide_Static();
                coverBTN.SetActive(false);
            }, (int score) =>
            {
                // Clicked okay
                coverBTN.SetActive(false);
                UIinputWindow.Show_Static("Player Name", "", "abcdefgýjklmnopqrstuvxyz", () =>
                {
                    // Clicked cancel
                    UI_Blocker.Hide_Static();
                }, (string nameText) =>
                {
                    // Clicked okay
                    UI_Blocker.Hide_Static();
                    highscoreTable.AddHighScoreEntry(score, nameText);
                    PlayerPrefs.DeleteKey("score");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                });
            });
        });
    }
}
