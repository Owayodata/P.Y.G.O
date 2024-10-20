using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class BallClone : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;
    public TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource bounceAudio;

    Rigidbody2D rb;

    void Start()
    {
        
        scoreText = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 7f;
    }

    void Update()
    {
       
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
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
            Ball.brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
            bounceAudio.Play();
            Destroy(collision.gameObject);
            Ball.score++;
            PlayerPrefs.SetInt("score", Ball.score);
            scoreText.text = Ball.score.ToString("0000");
            Ball.brickCount--;
        }
    }
}
