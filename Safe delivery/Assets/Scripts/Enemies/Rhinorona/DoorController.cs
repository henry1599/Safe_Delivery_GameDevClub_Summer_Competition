using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform pointUp;
    public Transform pointDown;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        if (RhinoSharedVariables.IS_ACTIVATE == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointUp.position, speed * Time.deltaTime);

        }
        if (RhinoSharedVariables.IS_DEATH == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointDown.position, speed * Time.deltaTime);
        }
    }
}
