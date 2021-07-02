using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaBehavior : MonoBehaviour
{
    public GameObject explorePrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explorePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
