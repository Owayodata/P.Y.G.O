using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDivisionPowerUP : MonoBehaviour
{
    [SerializeField] private GameObject ClonePrefab;
    private int numberOfClones = 2;
    private float distanceBetweenClones = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
            for (int i = 0; i < numberOfClones; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(i * distanceBetweenClones, 0f, 0f);
                Instantiate(ClonePrefab, spawnPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
