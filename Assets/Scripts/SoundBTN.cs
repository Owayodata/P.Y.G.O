using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBTN : MonoBehaviour
{
    private Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;
    private bool isOn = true;

    public GameObject audioSource;
    // Start is called before the first frame update
    void Start()
    {
        soundOnImage = button.image.sprite;
        audioSource = GameObject.FindWithTag("Music");
        audioSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        if (isOn)
        {
            button.image.sprite = soundOffImage;
            isOn = false;
            audioSource.gameObject.SetActive(false);
        } else
        {
            button.image.sprite = soundOnImage;
            isOn=true;
            audioSource.gameObject.SetActive(true);
        }
    }
}
