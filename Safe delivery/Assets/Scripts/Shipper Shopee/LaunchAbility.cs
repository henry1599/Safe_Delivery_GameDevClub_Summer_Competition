using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAbility : MonoBehaviour
{
    [Header("Launch Ability")]
    //private bool isChoosingDirection;
    public float launchPower;
    public float launchTime;
    public GameObject arrow;
    private Vector2 launchDirection;
    private float launchTimeReset;
    public GameObject arrowPivot;
    private Rigidbody2D rb;
    public GameObject launchEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        launchTimeReset = launchTime;
        ShareVariables.IS_LAUNCHING = false;
    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }

    //////////////////////////////////////-- LAUNCH
    void Launch()
    {
        if (ShareVariables.CURRENT_ENERGY < 10) return;
        if (ShareVariables.IS_BASHING == true) return;
        if (ShareVariables.ENABLE_LAUNCH == true)
        {
            if (Input.GetKey(KeyCode.B))
            {
                Shared.IS_ENABLE_TO_SWITCH = false;
                Time.timeScale = 0f;
                arrow.SetActive(true);
                arrow.transform.position = transform.transform.position;
            }
            else if (Input.GetKeyUp(KeyCode.B))
            {
                ShareVariables.IS_USING_ABILITY = true;
                ShareVariables.ENABLE_LAUNCH = false;
                Time.timeScale = 1;
                ShareVariables.IS_LAUNCHING = true;
                rb.velocity = Vector2.zero;
                launchDirection = arrowPivot.transform.position - transform.position;
                launchDirection = launchDirection.normalized;
                Instantiate(launchEffect, transform.position, Quaternion.identity);
                ShareVariables.EXTRA_JUMP = ShareVariables.EXTRA_JUMP_VALUE;
                arrow.SetActive(false);
                Shared.IS_ENABLE_TO_SWITCH = true;
            }

            
        }
        //// Perform bash movement
        if (ShareVariables.IS_LAUNCHING == true)
        {
            if (launchTime > 0)
            {
                if (Mathf.Abs(launchDirection.x) > Mathf.Abs(launchDirection.y))
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchPower, ForceMode2D.Force);
                }
                else
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchPower * 0.5f, ForceMode2D.Force);
                }

                launchTime -= Time.deltaTime;
            }
            else
            {
                rb.gravityScale = 30;
                ShareVariables.IS_LAUNCHING = false;
                launchTime = launchTimeReset;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

    }
}
