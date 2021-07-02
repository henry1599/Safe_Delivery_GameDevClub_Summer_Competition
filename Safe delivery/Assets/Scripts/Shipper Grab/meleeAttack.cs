using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    public Animator anim;
    public bool isAttack = false;
    public static meleeAttack instance;
    public GameObject attackPoint;
    [Range(0, 100)]
    public float Range; // 20 is good
    public LayerMask whatIsObstacle;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (isAttack == true)
        {
            Collider2D[] obstacles = Physics2D.OverlapCircleAll(attackPoint.transform.position, Range, whatIsObstacle);
            foreach (Collider2D obstacle in obstacles)
            {
                // Take damage
            }

        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.F) && !isAttack)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, Range);
    }
}
