using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && RhinoSharedVariables.IS_DEATH == false)
        {
            RhinoSharedVariables.IS_ACTIVATE = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            RhinoSharedVariables.IS_ACTIVATE = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && RhinoSharedVariables.IS_DEATH == true)
        {
            RhinoSharedVariables.IS_PLAYER_OUT = true;
            RhinoSharedVariables.IS_ACTIVATE = false;
        }
    }
}
