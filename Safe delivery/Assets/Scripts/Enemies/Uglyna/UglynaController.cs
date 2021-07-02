using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UglynaController : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    private Transform nextPos;
    public Transform currentPos;
    [Range(0, 1000)]
    public float speed;
    private bool isAttacking;
    public GameObject bulletPrefab;
    [Range(0, 10000)]
    public float bulletSpeed;
    [Range(0, 1000)]
    public float attackRange;
    private GameObject player;
    public Transform firePoint;
    private bool isShooting;
    public float timeBtwShotValue;
    private float timeBtwShot;
    [Range(0, 1000)]
    public float HP;
    Enemy e;
    // Start is called before the first frame update
    void Start()
    {
        e = transform.GetComponent<Enemy>();
        e.SetMaXHP(HP);
        nextPos = currentPos;
        isAttacking = false;
        isShooting = false;
        timeBtwShot = timeBtwShotValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Cast player in range
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D castedObject in playerInRange)
        {
            player = null;
            if (castedObject.gameObject.tag.Equals("Player"))
            {
                player = castedObject.gameObject;
                break;
            }
        }
        if (player == null)
        {
            timeBtwShot = timeBtwShotValue;
            isAttacking = false;
            isShooting = false;
        }
        else
        {
            isAttacking = true;
            isShooting = true;
        }
        // Perform move
        if (isAttacking == false)
        {
            if (transform.position.x <= point1.position.x)
            {
                nextPos = point2;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (transform.position.x >= point2.position.x)
            {
                nextPos = point1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        }
        // Perform attack
        else
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (isShooting == true)
            {
                if (timeBtwShot > 0)
                {
                    timeBtwShot -= Time.deltaTime;
                }
                else
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().AddForce((player.transform.position - firePoint.position).normalized * bulletSpeed * 10, ForceMode2D.Force);
                    Destroy(bullet, 5f);
                    isShooting = false;
                    isAttacking = false;
                    timeBtwShot = timeBtwShotValue;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Bullet"))
        {
            e.TakeDamage(Random.Range(10, 30));
        }
    }
}
