using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public GameObject explore;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explore, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
