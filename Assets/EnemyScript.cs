using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float randomAttackDelay;
    public GameObject projectile;
    void Start()
    {
        randomAttackDelay = Random.Range(1f, 4f);
    }


    void Update()
    {
        randomAttackDelay -= Time.deltaTime;
        if (randomAttackDelay <= 0)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            randomAttackDelay = Random.Range(1f, 4f);
        }
    }
}
