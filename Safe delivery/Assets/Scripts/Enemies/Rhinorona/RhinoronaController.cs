using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoronaController : MonoBehaviour
{
    [Range(0, 1000)]
    public float maxHP;
    Enemy e;
    private Rigidbody2D rb;
    public List<Transform> players = new List<Transform>();
    public GameObject horn;
    [Range(0, 1000)]
    public float speed;
    private Vector2 direction;
    public GameObject heathBar;
    // Start is called before the first frame update
    void Start()
    {
        RhinoSharedVariables.IS_DEATH = false;
        heathBar.GetComponent<HealthBar>().SetMaxValue(RhinoSharedVariables.HEATH);
        rb = transform.GetComponent<Rigidbody2D>();
        e = transform.GetComponent<Enemy>();
        e.SetMaXHP(RhinoSharedVariables.HEATH);
    }

    // Update is called once per frame
    void Update()
    {
        if (RhinoSharedVariables.IS_ACTIVATE == true)
        {
            heathBar.SetActive(true);
        }
        Attack();
    }

    void Attack()
    {
        if (RhinoSharedVariables.IS_ATTACKING == true)
        {
            rb.velocity = direction;
        }
        // Perform the attack
        if (players[Shared.CURRENT_SHIPPER].position.x < transform.position.x && RhinoSharedVariables.IS_IN_RANGE == true)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = Vector2.left * speed;
        }
        else if (players[Shared.CURRENT_SHIPPER].position.x > transform.position.x && RhinoSharedVariables.IS_IN_RANGE == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = Vector2.right * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Bullet"))
        {
            float damage = Random.Range(10, 30);
            e.TakeDamage(damage);
            RhinoSharedVariables.HEATH -= damage;
            heathBar.GetComponent<HealthBar>().SetValue(RhinoSharedVariables.HEATH);
            if (RhinoSharedVariables.HEATH <= 0)
            {
                RhinoSharedVariables.IS_DEATH = true;
                RhinoSharedVariables.IS_ACTIVATE = false;
                heathBar.SetActive(false);
            }
        }
    }
}
