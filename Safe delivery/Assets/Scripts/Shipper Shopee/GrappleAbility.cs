using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAbility : MonoBehaviour
{
    [Header("Grapple ability")]
    [Range(0, 10000)]
    public float grapplePower;
    private Rigidbody2D rb;
    [Range(0, 1000)]
    public float radiusGrapple;
    private GameObject GrappableObject;
    private bool nearToGrapple;
    public GameObject arrow;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Grapple();
        // Use move position in rb
    }

    void Grapple()
    {
        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, radiusGrapple, Vector3.forward);
        foreach (RaycastHit2D item in Rays)
        {
            nearToGrapple = false;
            if (item.collider.tag.Equals("Bashable"))
            {
                nearToGrapple = true;
                GrappableObject = item.collider.transform.gameObject;
                break;
            }
        }
        if (nearToGrapple == true)
        {
            arrow.SetActive(true);
            Vector2 direction = GrappableObject.transform.position - arrow.transform.position;
            float angleToRotate = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            arrow.transform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);
            if (Input.GetKeyDown(KeyCode.G))
            {
                rb.MovePosition(GrappableObject.transform.position * 6 / 5);
                nearToGrapple = false;
                arrow.SetActive(false);
                GrappableObject = null;
            }
        }
        else if (GrappableObject != null)
        {
            nearToGrapple = false;
            arrow.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusGrapple);
    }
}
