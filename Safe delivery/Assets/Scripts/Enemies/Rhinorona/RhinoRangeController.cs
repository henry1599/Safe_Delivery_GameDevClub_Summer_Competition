using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoRangeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_ATTACKING = true;
            RhinoSharedVariables.IS_IN_RANGE = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_ATTACKING = true;
            RhinoSharedVariables.IS_IN_RANGE = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_IN_RANGE = false;
        }
    }
}
