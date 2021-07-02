using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public HealthBar healthBar;
    [Range(0, 1000)]
    public float maxHP;
    private float currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxValue(maxHP);
    }

    private void Update()
    {
        if (currentHP <= 0)
        {
            Shared.SHIPPER_STATE[Shared.CURRENT_SHIPPER] = false;
            // Perform switch to the next shipper
            SwitchingShipper.Instance.SwitchImmediately();
        }
        if (Shared.IS_ALL_DEATH == true)
        {
            Debug.Log("All death, Game over");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Enemy Bullet") && gameObject.activeSelf)
        {
            currentHP -= Random.Range(40, 50);
        }
        if (collision.collider.tag.Equals("Enemy") && gameObject.activeSelf)
        {
            currentHP -= Random.Range(20, 30);
        }
        if (collision.collider.tag.Equals("Rhino horn") && gameObject.activeSelf)
        {
            currentHP -= Random.Range(50, 60);
        }
        if (collision.collider.tag.Equals("Rhino boss") && gameObject.activeSelf)
        {
            currentHP -= Random.Range(50, 60);
        }
        healthBar.SetValue(currentHP);
    }
}
