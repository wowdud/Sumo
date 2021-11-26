using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float speed = 10;
    public float lifespan = 1.75f;
    bool flipDirection;
    Vector2 archerPos;

    public void Start()
    {
        archerPos = gameObject.transform.position;
        if (PlayerScript.playerPos.x > archerPos.x)
        {
            flipDirection = false;
        }
        else
        {
            flipDirection = true;
        }
    }

    public void Update()
    {
        if (flipDirection)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    */
}