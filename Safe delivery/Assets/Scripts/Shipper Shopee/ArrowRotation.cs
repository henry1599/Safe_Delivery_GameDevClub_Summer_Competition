using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public float speed;
    private float movement;
    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position);
        //float angleToRotate = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);
        movement = Input.GetAxisRaw("Horizontal");
        transform.RotateAround(ShareVariables.BASH_ABLE_OBJECT, Vector3.forward, movement * -speed);
    }
}
