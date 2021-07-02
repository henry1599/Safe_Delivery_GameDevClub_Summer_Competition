using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explore;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.tag.Equals("Player"))
            Instantiate(explore, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player"))
            Instantiate(explore, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
