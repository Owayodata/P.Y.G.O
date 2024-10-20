using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighscoreEntryContainer");
        Debug.Log(entryContainer); // Corrected Debug Log

        if (entryContainer == null)
        {
            Debug.LogError("entryContainer not found");
            return;
        }

        entryTemplate = entryContainer.Find("HighscoreEntryTemplate");
        Debug.Log(entryTemplate); // Corrected Debug Log

        if (entryTemplate == null)
        {
            Debug.LogError("entryTemplate not found");
            return;
        }

        entryTemplate.gameObject.SetActive(false);

        //AddHighScoreEntry(200000000, "CMK");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null || highscores.highscoreEntryList == null)
        {
            Debug.LogError("No highscore data found");
            return;
        }

        // Sort the scores according to which one has a higher value
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        if (highscores.highscoreEntryList.Count > 10)
        {
            for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
            {
                highscores.highscoreEntryList.RemoveAt(10);
            }
        }

        highScoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 120f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("PosTxt").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreTxt").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameTxt").GetComponent<TMP_Text>().text = name;


        //set background odds and evens easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        if(rank == 1)
        {
            //highlight first
            entryTransform.Find("PosTxt").GetComponent<TMP_Text>().color = Color.cyan;
            entryTransform.Find("ScoreTxt").GetComponent<TMP_Text>().color = Color.cyan;
            entryTransform.Find("NameTxt").GetComponent<TMP_Text>().color = Color.cyan;
        }

        transformList.Add(entryTransform);
    }

    public void AddHighScoreEntry(int score, string name)
    {
        // Create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }

        // Add entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated highscores
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        if (highscores.highscoreEntryList.Count > 10)
        {
            for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
            {
                highscores.highscoreEntryList.RemoveAt(10);
            }
        }
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    // Represents one highscore entry
    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
