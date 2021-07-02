using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornController : MonoBehaviour
{
    bool isSpawn = false;
    public float timeBtwSpawnValue;
    private float timeBtwSpawn;
    private void Start()
    {
        timeBtwSpawn = timeBtwSpawnValue;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Environment"))
        {
            RhinoSharedVariables.IS_ATTACKING = false;
            isSpawn = true;
        }
        if (collision.collider.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_HIT_PLAYER = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_HIT_PLAYER = false;
        }
        if (collision.collider.tag.Equals("Environment"))
        {
            isSpawn = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_HIT_PLAYER = false;
        }
        if (collision.collider.tag.Equals("Environment"))
        {
            if (isSpawn == true)
            {
                if (timeBtwSpawn > 0)
                {
                    timeBtwSpawn -= Time.deltaTime;
                }
                else
                {
                    SpawnCorona.Instance.Spawn();
                    timeBtwSpawn = timeBtwSpawnValue;
                }
            }
        }
    }
}
