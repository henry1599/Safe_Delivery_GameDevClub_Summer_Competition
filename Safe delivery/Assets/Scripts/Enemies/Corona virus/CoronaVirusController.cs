using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaVirusController : MonoBehaviour
{
    [Range(0, 1000)]
    public float HP;
    Enemy e;
    private void Start()
    {
        e = transform.GetComponent<Enemy>();
        e.SetMaXHP(HP);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            e.TakeDamage(Random.Range(10, 20));
        }
    }
}
