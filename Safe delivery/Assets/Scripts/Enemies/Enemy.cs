using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float HP;
    public GameObject explore;
    public void SetMaXHP(float _HP)
    {
        HP = _HP;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Instantiate(explore, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
