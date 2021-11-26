using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScipt : MonoBehaviour
{
    public float speed = 10;
    public float lifespan = 1.75f;
    bool flipDirection;


    public void Start()
    {
        if (!PlayerScript.isPlayerFlipped)
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
        if (!flipDirection)
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
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
