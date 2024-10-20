using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSCRN : MonoBehaviour
{

    [SerializeField] private GameObject PausePNL;
    private Sprite pauseimg;
    public Sprite resumeimg;
    public Button pausebtn;
    private bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseimg = pausebtn.image.sprite;
    }

    public void Paused()
    {
        if (pause)
        {
            pause = false;
            PausePNL.SetActive(false);
            pausebtn.image.sprite = pauseimg;
            Time.timeScale = 1;
        } else
        {
            pause = true;
            PausePNL.SetActive(true);
            pausebtn.image.sprite = resumeimg;
            Time.timeScale = 0;

        }
    }
}
