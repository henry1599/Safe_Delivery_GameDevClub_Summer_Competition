using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLaunchRotation : MonoBehaviour
{
    public float speed;
    private float movement;
    public Transform player;
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        transform.RotateAround(player.position, Vector3.forward, movement * -speed);
    }
}
