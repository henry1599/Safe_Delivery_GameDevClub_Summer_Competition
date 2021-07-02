using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushInteraction : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            anim.SetBool("isTouching", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            anim.SetBool("isTouching", false);
        }
    }
}
