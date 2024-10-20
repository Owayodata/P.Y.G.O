using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;
    public static int score = 0;
    public static int scoreReset;
    int lives = 5;
    public TextMeshProUGUI scoreText;
    public GameObject[] livesImg;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public static int brickCount;
    [SerializeField] private AudioSource bounceAudio;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        scoreReset = PlayerPrefs.GetInt("score");
        bounceAudio = GetComponent<AudioSource>();
        score = PlayerPrefs.GetInt("score", 0); // Retrieve and assign the stored score
        scoreText.text = score.ToString("0000"); // Update the score display
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 7f;
        brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            if (lives <= 0)
            {
                GameOver();
            }
            else
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector2.down * 7f;
                lives--;
                livesImg[lives].SetActive(false);
            }
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            bounceAudio.Play();
            Destroy(collision.gameObject);
            score++;
            PlayerPrefs.SetInt("score", score);
            scoreText.text = score.ToString("0000");
            brickCount--;
            if (brickCount <= 0)
            {
                youWinPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        Destroy(gameObject);
    }
}
