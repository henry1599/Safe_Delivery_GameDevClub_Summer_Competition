using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public List<Transform> habitats;
    private Rigidbody2D rb;
    [Range(0,1000)]
    public float speed;
    [Range(0,1000)]
    public float attackRange;
    public GameObject bulletPrefab;
    [Range(0, 1000)]
    public float bulletSpeed;
    private bool isBoss;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        follow();
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attack();
        }
    }

    void follow()
    {
        rb.velocity = (habitats[SwitchingShipper.Instance.currentIdx].position - transform.position).normalized * speed;
    }

    void Attack()
    {
        Collider2D[] EnemyInRange = Physics2D.OverlapCircleAll(transform.position, attackRange);
        GameObject singleEnemy = null;
        foreach (Collider2D enemy in EnemyInRange)
        {
            if (enemy.gameObject.tag.Equals("Enemy"))
            {
                isBoss = false;
                singleEnemy = enemy.gameObject;
                break;
            }
            else if (enemy.gameObject.tag.Equals("Rhino boss"))
            {
                isBoss = true;
                singleEnemy = enemy.gameObject;
                break;
            }
        }
        if (singleEnemy != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            if (isBoss == true)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = (singleEnemy.transform.position - bullet.transform.position).normalized * bulletSpeed * 10;
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().velocity = (singleEnemy.transform.position - bullet.transform.position).normalized * bulletSpeed;
            }
            Destroy(bullet, 3f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
