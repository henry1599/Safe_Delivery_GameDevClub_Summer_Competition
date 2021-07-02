using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashAbility : MonoBehaviour
{
    [Header("Bash Ability")]
    public float RadiusBash;
    private GameObject BashAbleObj;
    private bool nearToBashAbleObj;
    //private bool isChoosingDirection;
    public float bashPower;
    public float bashTime;
    public GameObject arrow;
    private Vector2 bashDirection;
    private float bashTimeReset;
    public GameObject arrowPivot;
    private Rigidbody2D rb;
    public GameObject bashEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        bashTimeReset = bashTime;
        ShareVariables.IS_BASHING = false;
    }

    // Update is called once per frame
    void Update()
    {
        Bash();
    }

    //////////////////////////////////////-- BASH
    void Bash()
    {
        if (ShareVariables.CURRENT_ENERGY < 10) return;
        if (ShareVariables.IS_LAUNCHING == true) return;
        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, RadiusBash, Vector3.forward);
        foreach (RaycastHit2D ray in Rays)
        {
            nearToBashAbleObj = false;
            if (ray.collider.tag.Equals("Bashable") || ray.collider.tag.Equals("Enemy") || ray.collider.tag.Equals("Enemy Bullet"))
            {
                nearToBashAbleObj = true;
                BashAbleObj = ray.collider.transform.gameObject;
                break;
            }
        }
        if (nearToBashAbleObj == true)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Shared.IS_ENABLE_TO_SWITCH = false;
                Time.timeScale = 0f;
                //BashAbleObj.transform.localScale = new Vector2(1.4f, 1.4f);
                ShareVariables.BASH_ABLE_OBJECT = BashAbleObj.transform.position;
                arrow.SetActive(true);
                arrow.transform.position = BashAbleObj.transform.transform.position;
                //isChoosingDirection = true;
            }
            else if (/*isChoosingDirection == true && */Input.GetKeyUp(KeyCode.F))
            {
                ShareVariables.IS_USING_ABILITY = true;
                Time.timeScale = 1;
                //BashAbleObj.transform.localScale = new Vector2(1, 1);
                //isChoosingDirection = false;
                ShareVariables.IS_BASHING = true;
                rb.velocity = Vector2.zero;
                //transform.position = BashAbleObj.transform.position;
                bashDirection = arrowPivot.transform.position - BashAbleObj.transform.position;
                //bashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - BashAbleObj.transform.position;
                //bashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                bashDirection = bashDirection.normalized;
                BashAbleObj.GetComponent<Rigidbody2D>().AddForce(-bashDirection * bashPower, ForceMode2D.Force);
                Instantiate(bashEffect, BashAbleObj.transform.position, Quaternion.identity);
                ShareVariables.EXTRA_JUMP = ShareVariables.EXTRA_JUMP_VALUE;
                arrow.SetActive(false);
                Shared.IS_ENABLE_TO_SWITCH = true;
            }
        }

        //// Perform bash movement
        if (ShareVariables.IS_BASHING == true)
        {
            ShareVariables.ENABLE_LAUNCH = true;
            if (bashTime > 0)
            {
                if (Mathf.Abs(bashDirection.x) > Mathf.Abs(bashDirection.y))
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(bashDirection * bashPower, ForceMode2D.Force);
                    //rb.MovePosition(bashDirection * 100);
                }
                else
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(bashDirection * bashPower * 0.5f, ForceMode2D.Force);
                    //rb.MovePosition(bashDirection * 100);
                }
                    
                bashTime -= Time.deltaTime;
                //rb.AddForce(bashDirection * bashPower, ForceMode2D.Impulse);
            }
            else
            {
                rb.gravityScale = 30;
                ShareVariables.IS_BASHING = false;
                bashTime = bashTimeReset;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RadiusBash);
    }
    
}
